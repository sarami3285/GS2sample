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
 */

// ReSharper disable ConvertSwitchStatementToSwitchExpression

#pragma warning disable CS1522 // Empty switch block

using System;
using Gs2.Core.Domain;
using Gs2.Core.Net;
using Gs2.Core.Util;
#if UNITY_2017_1_OR_NEWER
using System.Collections;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
    #endif
#else
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Matchmaking.Model.Cache
{
    public static partial class JoinedSeasonGatheringExt
    {
        public static string CacheParentKey(
            this JoinedSeasonGathering self,
            string namespaceName,
            string userId,
            string seasonName,
            long? season
        ) {
            return string.Join(
                ":",
                "matchmaking",
                namespaceName,
                userId,
                seasonName,
                season.ToString(),
                "JoinedSeasonGathering"
            );
        }

        public static string CacheKey(
            this JoinedSeasonGathering self
        ) {
            return "Singleton";
        }

#if UNITY_2017_1_OR_NEWER
        public static IFuture<JoinedSeasonGathering> FetchFuture(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season,
            Func<IFuture<JoinedSeasonGathering>> fetchImpl
        ) {
            IEnumerator Impl(IFuture<JoinedSeasonGathering> self)
            {
                var future = fetchImpl();
                yield return future;
                if (future.Error != null)
                {
                    if (future.Error is Gs2.Core.Exception.NotFoundException e)
                    {
                        (null as JoinedSeasonGathering).PutCache(
                            cache,
                            namespaceName,
                            userId,
                            seasonName,
                            season
                        );
                        if (e.Errors.Length != 0 && e.Errors[0].Component == "joinedSeasonGathering") {
                            self.OnComplete(default);
                            yield break;
                        }
                    }
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                item.PutCache(
                    cache,
                    namespaceName,
                    userId,
                    seasonName,
                    season
                );
                self.OnComplete(item);
            }
            return new Gs2InlineFuture<JoinedSeasonGathering>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public static async UniTask<JoinedSeasonGathering> FetchAsync(
    #else
        public static async Task<JoinedSeasonGathering> FetchAsync(
    #endif
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season,
    #if UNITY_2017_1_OR_NEWER
            Func<UniTask<JoinedSeasonGathering>> fetchImpl
    #else
            Func<Task<JoinedSeasonGathering>> fetchImpl
    #endif
        ) {
            using (await cache.GetLockObject<JoinedSeasonGathering>(
                       self.CacheParentKey(
                            namespaceName,
                            userId,
                            seasonName,
                            season
                       ),
                       self.CacheKey(
                       )
                   ).LockAsync()) {
                try {
                    var item = await fetchImpl();
                    item.PutCache(
                        cache,
                        namespaceName,
                        userId,
                        seasonName,
                        season
                    );
                    return item;
                }
                catch (Gs2.Core.Exception.NotFoundException e) {
                    (null as JoinedSeasonGathering).PutCache(
                        cache,
                        namespaceName,
                        userId,
                        seasonName,
                        season
                    );
                    if (e.errors.Length == 0 || e.errors[0].component != "joinedSeasonGathering") {
                        throw;
                    }
                    return null;
                }
            }
        }
#endif

        public static Tuple<JoinedSeasonGathering, bool> GetCache(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            return cache.Get<JoinedSeasonGathering>(
                self.CacheParentKey(
                    namespaceName,
                    userId,
                    seasonName,
                    season
                ),
                self.CacheKey(
                )
            );
        }

        public static void PutCache(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            cache.Put(
                self.CacheParentKey(
                    namespaceName,
                    userId,
                    seasonName,
                    season
                ),
                self.CacheKey(
                ),
                self,
                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
            );
        }

        public static void DeleteCache(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            cache.Delete<JoinedSeasonGathering>(
                self.CacheParentKey(
                    namespaceName,
                    userId,
                    seasonName,
                    season
                ),
                self.CacheKey(
                )
            );
        }

        public static void ListSubscribe(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season,
            Action<JoinedSeasonGathering[]> callback
        ) {
            cache.ListSubscribe<JoinedSeasonGathering>(
                self.CacheParentKey(
                    namespaceName,
                    userId,
                    seasonName,
                    season
                ),
                callback
            );
        }

        public static void ListUnsubscribe(
            this JoinedSeasonGathering self,
            CacheDatabase cache,
            string namespaceName,
            string userId,
            string seasonName,
            long? season,
            ulong callbackId
        ) {
            cache.ListUnsubscribe<JoinedSeasonGathering>(
                self.CacheParentKey(
                    namespaceName,
                    userId,
                    seasonName,
                    season
                ),
                callbackId
            );
        }
    }
}