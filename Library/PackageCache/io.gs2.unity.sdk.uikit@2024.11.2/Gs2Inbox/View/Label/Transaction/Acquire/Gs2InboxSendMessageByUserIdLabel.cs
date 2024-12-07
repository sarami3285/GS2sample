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
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using Gs2.Gs2Inbox.Request;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Inbox.Context;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inbox/Message/View/Label/Transaction/Gs2InboxSendMessageByUserIdLabel")]
    public partial class Gs2InboxSendMessageByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Message == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{metadata}",
                    $"{this._fetcher.Request.Metadata}"
                ).Replace(
                    "{readAcquireActions}",
                    $"{this._fetcher.Request.ReadAcquireActions}"
                ).Replace(
                    "{expiresAt}",
                    $"{this._fetcher.Request.ExpiresAt}"
                ).Replace(
                    "{expiresTimeSpan}",
                    $"{this._fetcher.Request.ExpiresTimeSpan}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:messageId}",
                    $"{this._userDataFetcher.Message.MessageId}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.Message.Name}"
                ).Replace(
                    "{userData:metadata}",
                    $"{this._userDataFetcher.Message.Metadata}"
                ).Replace(
                    "{userData:isRead}",
                    $"{this._userDataFetcher.Message.IsRead}"
                ).Replace(
                    "{userData:readAcquireActions}",
                    $"{this._userDataFetcher.Message.ReadAcquireActions}"
                ).Replace(
                    "{userData:receivedAt}",
                    $"{this._userDataFetcher.Message.ReceivedAt}"
                ).Replace(
                    "{userData:readAt}",
                    $"{this._userDataFetcher.Message.ReadAt}"
                ).Replace(
                    "{userData:expiresAt}",
                    $"{this._userDataFetcher.Message.ExpiresAt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        private Gs2InboxSendMessageByUserIdFetcher _fetcher;
        private Gs2InboxOwnMessageFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2InboxSendMessageByUserIdFetcher>() ?? GetComponentInParent<Gs2InboxSendMessageByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxSendMessageByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InboxSendMessageByUserIdFetcher>() ?? GetComponentInParent<Gs2InboxSendMessageByUserIdFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);
            if (this._fetcher.Fetched) {
                OnFetched();
            }
            if (this._userDataFetcher != null) {
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
                if (this._userDataFetcher.Fetched) {
                    OnFetched();
                }
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                if (this._userDataFetcher != null) {
                    this._userDataFetcher.OnFetched.RemoveListener(this._onFetched);
                }
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}