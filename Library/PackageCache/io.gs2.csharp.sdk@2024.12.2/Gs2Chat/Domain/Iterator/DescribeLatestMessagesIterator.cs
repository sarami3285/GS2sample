
/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 *
 * deny overwrite
 */
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ArrangeThisQualifier
// ReSharper disable NotAccessedField.Local
// ReSharper disable InconsistentNaming

#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable 1998

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Util;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Gs2Chat.Model.Cache;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
    #if GS2_ENABLE_UNITASK
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
    #else
using System.Collections;
using UnityEngine.Events;
    #endif
#else
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Chat.Domain.Iterator
{

    #if UNITY_2017_1_OR_NEWER
    public class DescribeLatestMessagesIterator : Gs2Iterator<Gs2.Gs2Chat.Model.Message> {
    #else
    public class DescribeLatestMessagesIterator : IAsyncEnumerable<Gs2.Gs2Chat.Model.Message> {
    #endif
        private readonly Gs2.Core.Domain.Gs2 _gs2;
        private readonly Gs2ChatRestClient _client;
        public string NamespaceName { get; }
        public string RoomName { get; }
        public string Password { get; }
        public AccessToken AccessToken { get; }
        public string UserId => AccessToken?.UserId;
        private long? _startAt;
        private bool _isCacheChecked;
        private bool _last;
        private Gs2.Gs2Chat.Model.Message[] _result;

        int? fetchSize;

        public DescribeLatestMessagesIterator(
            Gs2.Core.Domain.Gs2 gs2,
            Gs2ChatRestClient client,
            string namespaceName,
            string roomName,
            AccessToken accessToken,
            string password = null
        ) {
            this._gs2 = gs2;
            this._client = client;
            this.NamespaceName = namespaceName;
            this.RoomName = roomName;
            this.Password = password;
            this.AccessToken = accessToken;
            this._startAt = null;
            this._last = false;
            this._result = new Gs2.Gs2Chat.Model.Message[]{};

            this.fetchSize = null;
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask _load() {
            #else
        private IEnumerator _load() {
            #endif
        #else
        private async Task _load() {
        #endif
            var isCacheChecked = this._isCacheChecked;
            this._isCacheChecked = true;
            if (!isCacheChecked && this._gs2.Cache.TryGetList
                    <Gs2.Gs2Chat.Model.Message>
            (
                    (null as Gs2.Gs2Chat.Model.Message).CacheParentKey(
                        NamespaceName,
                        AccessToken?.UserId,
                        RoomName
                    ),
                    out var list,
                    out var listCacheContext
            )) {
                if (listCacheContext == null)
                {
                    this._result = list;
                    this._last = true;
                }
                else
                {
                    this._startAt = (long)listCacheContext;
                    this._result = list.Where(message => message.CreatedAt < this._startAt).ToArray();
                    this._last = false;
                    this._gs2.Cache.ClearListCache<Gs2.Gs2Chat.Model.Message>(
                        (null as Gs2.Gs2Chat.Model.Message).CacheParentKey(
                            NamespaceName,
                            AccessToken?.UserId,
                            RoomName
                        )
                    );
                }
            } else {

                #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                var future = this._client.DescribeLatestMessagesFuture(
                #else
                var r = await this._client.DescribeLatestMessagesAsync(
                #endif
                    new Gs2.Gs2Chat.Request.DescribeLatestMessagesRequest()
                        .WithContextStack(this._gs2.DefaultContextStack)
                        .WithNamespaceName(this.NamespaceName)
                        .WithRoomName(this.RoomName)
                        .WithPassword(this.Password)
                        .WithAccessToken(this.AccessToken != null ? this.AccessToken.Token : null)
                        .WithLimit(this.fetchSize)
                );
                #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                yield return future;
                if (future.Error != null)
                {
                    Error = future.Error;
                    yield break;
                }
                var r = future.Result;
                #endif
                this._result = r.Items
                    .ToArray();
                if (this._result.Length > 0) {
                    this._startAt = this._result[this._result.Length-1].CreatedAt + 1;
                } else {
                    this._last = true;
                }
                foreach (var item in r.Items) {
                    item.PutCache(
                        this._gs2.Cache,
                        NamespaceName,
                        AccessToken?.UserId,
                        RoomName,
                        item.Name
                    );
                }

                if (this._last) {
                    this._gs2.Cache.SetListCached<Gs2.Gs2Chat.Model.Message>(
                        (null as Gs2.Gs2Chat.Model.Message).CacheParentKey(
                            NamespaceName,
                            AccessToken?.UserId,
                            RoomName
                        ),
                        this._startAt
                    );
                }
            }
        }

        private bool _hasNext()
        {
            return true;
        }

        #if UNITY_2017_1_OR_NEWER
        public override bool HasNext()
        {
            if (Error != null) return false;
            return _hasNext();
        }
        #endif

        #if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK

        protected override System.Collections.IEnumerator Next(
            Action<AsyncResult<Gs2.Gs2Chat.Model.Message>> callback
        )
        {
            Gs2Exception error = null;
            yield return UniTask.ToCoroutine(
                async () => {
                    try {
                        if (this._result.Length == 0 && !this._last) {
                            await this._load();
                        }
                        if (this._result.Length == 0) {
                            Current = null;
                            return;
                        }
                        var ret = this._result[0];
                        this._result = this._result.ToList().GetRange(1, this._result.Length - 1).ToArray();
                        if (this._result.Length == 0 && !this._last) {
                            await this._load();
                        }
                        Current = ret;
                    }
                    catch (Gs2Exception e) {
                        Current = null;
                        error = e;
                    }
                }
            );
            callback.Invoke(new AsyncResult<Gs2.Gs2Chat.Model.Message>(
                Current,
                error
            ));
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Chat.Model.Message> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
            #else

        protected override IEnumerator Next(
            Action<AsyncResult<Gs2.Gs2Chat.Model.Message>> callback
            #endif
        #else
        public async IAsyncEnumerator<Gs2.Gs2Chat.Model.Message> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
        #endif
        )
        {
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Gs2Chat.Model.Message>(async (writer, token) =>
            {
            #endif
        #endif
        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            while(this._hasNext()) {
        #endif
                if (this._result.Length == 0 && !this._last) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return this._load();
        #else
                    await this._load();
        #endif
                }
                if (this._result.Length == 0) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    Current = null;
                    callback.Invoke(new AsyncResult<Gs2.Gs2Chat.Model.Message>(
                        Current,
                        Error
                    ));
                    yield break;
        #else
                    break;
        #endif
                }
                var ret = this._result[0];
                this._result = this._result.ToList().GetRange(1, this._result.Length - 1).ToArray();
                if (this._result.Length == 0 && !this._last) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return this._load();
        #else
                    await this._load();
        #endif
                }
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
                await writer.YieldAsync(ret);
            #else
                Current = ret;
                callback.Invoke(new AsyncResult<Gs2.Gs2Chat.Model.Message>(
                    Current,
                    Error
                ));
            #endif
        #else
                yield return ret;
        #endif
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            }
            });
            #endif
        #else
            }
        #endif
        }
    }
}