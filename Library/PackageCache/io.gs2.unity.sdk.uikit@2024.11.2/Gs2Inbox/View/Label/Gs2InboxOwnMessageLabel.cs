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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inbox/Message/View/Label/Gs2InboxOwnMessageLabel")]
    public partial class Gs2InboxOwnMessageLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var receivedAt = this._fetcher.Message.ReceivedAt == null ? DateTime.Now : _fetcher.Message.ReceivedAt.ToLocalTime();
            var readAt = this._fetcher.Message.ReadAt == null ? DateTime.Now : _fetcher.Message.ReadAt.ToLocalTime();
            var expiresAt = this._fetcher.Message.ExpiresAt == null ? DateTime.Now : _fetcher.Message.ExpiresAt.ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{messageId}", $"{this._fetcher?.Message?.MessageId}"
                ).Replace(
                    "{name}", $"{this._fetcher?.Message?.Name}"
                ).Replace(
                    "{metadata}", $"{this._fetcher?.Message?.Metadata}"
                ).Replace(
                    "{isRead}", $"{this._fetcher?.Message?.IsRead}"
                ).Replace(
                    "{readAcquireActions}", $"{this._fetcher?.Message?.ReadAcquireActions}"
                ).Replace(
                    "{receivedAt:yyyy}", receivedAt.ToString("yyyy")
                ).Replace(
                    "{receivedAt:yy}", receivedAt.ToString("yy")
                ).Replace(
                    "{receivedAt:MM}", receivedAt.ToString("MM")
                ).Replace(
                    "{receivedAt:MMM}", receivedAt.ToString("MMM")
                ).Replace(
                    "{receivedAt:dd}", receivedAt.ToString("dd")
                ).Replace(
                    "{receivedAt:hh}", receivedAt.ToString("hh")
                ).Replace(
                    "{receivedAt:HH}", receivedAt.ToString("HH")
                ).Replace(
                    "{receivedAt:tt}", receivedAt.ToString("tt")
                ).Replace(
                    "{receivedAt:mm}", receivedAt.ToString("mm")
                ).Replace(
                    "{receivedAt:ss}", receivedAt.ToString("ss")
                ).Replace(
                    "{readAt:yyyy}", readAt.ToString("yyyy")
                ).Replace(
                    "{readAt:yy}", readAt.ToString("yy")
                ).Replace(
                    "{readAt:MM}", readAt.ToString("MM")
                ).Replace(
                    "{readAt:MMM}", readAt.ToString("MMM")
                ).Replace(
                    "{readAt:dd}", readAt.ToString("dd")
                ).Replace(
                    "{readAt:hh}", readAt.ToString("hh")
                ).Replace(
                    "{readAt:HH}", readAt.ToString("HH")
                ).Replace(
                    "{readAt:tt}", readAt.ToString("tt")
                ).Replace(
                    "{readAt:mm}", readAt.ToString("mm")
                ).Replace(
                    "{readAt:ss}", readAt.ToString("ss")
                ).Replace(
                    "{expiresAt:yyyy}", expiresAt.ToString("yyyy")
                ).Replace(
                    "{expiresAt:yy}", expiresAt.ToString("yy")
                ).Replace(
                    "{expiresAt:MM}", expiresAt.ToString("MM")
                ).Replace(
                    "{expiresAt:MMM}", expiresAt.ToString("MMM")
                ).Replace(
                    "{expiresAt:dd}", expiresAt.ToString("dd")
                ).Replace(
                    "{expiresAt:hh}", expiresAt.ToString("hh")
                ).Replace(
                    "{expiresAt:HH}", expiresAt.ToString("HH")
                ).Replace(
                    "{expiresAt:tt}", expiresAt.ToString("tt")
                ).Replace(
                    "{expiresAt:mm}", expiresAt.ToString("mm")
                ).Replace(
                    "{expiresAt:ss}", expiresAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InboxOwnMessageLabel
    {
        private Gs2InboxOwnMessageFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxOwnMessageFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>(true);
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
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxOwnMessageLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InboxOwnMessageLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxOwnMessageLabel
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