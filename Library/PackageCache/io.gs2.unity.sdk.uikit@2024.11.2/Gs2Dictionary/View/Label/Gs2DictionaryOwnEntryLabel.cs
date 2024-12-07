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
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/Entry/View/Label/Gs2DictionaryOwnEntryLabel")]
    public partial class Gs2DictionaryOwnEntryLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var acquiredAt = this._fetcher.Entry.AcquiredAt == null ? DateTime.Now : _fetcher.Entry.AcquiredAt.ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{entryId}", $"{this._fetcher?.Entry?.EntryId}"
                ).Replace(
                    "{userId}", $"{this._fetcher?.Entry?.UserId}"
                ).Replace(
                    "{name}", $"{this._fetcher?.Entry?.Name}"
                ).Replace(
                    "{acquiredAt:yyyy}", acquiredAt.ToString("yyyy")
                ).Replace(
                    "{acquiredAt:yy}", acquiredAt.ToString("yy")
                ).Replace(
                    "{acquiredAt:MM}", acquiredAt.ToString("MM")
                ).Replace(
                    "{acquiredAt:MMM}", acquiredAt.ToString("MMM")
                ).Replace(
                    "{acquiredAt:dd}", acquiredAt.ToString("dd")
                ).Replace(
                    "{acquiredAt:hh}", acquiredAt.ToString("hh")
                ).Replace(
                    "{acquiredAt:HH}", acquiredAt.ToString("HH")
                ).Replace(
                    "{acquiredAt:tt}", acquiredAt.ToString("tt")
                ).Replace(
                    "{acquiredAt:mm}", acquiredAt.ToString("mm")
                ).Replace(
                    "{acquiredAt:ss}", acquiredAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryOwnEntryLabel
    {
        private Gs2DictionaryOwnEntryFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2DictionaryOwnEntryFetcher>() ?? GetComponentInParent<Gs2DictionaryOwnEntryFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryOwnEntryFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2DictionaryOwnEntryFetcher>() ?? GetComponentInParent<Gs2DictionaryOwnEntryFetcher>(true);
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

    public partial class Gs2DictionaryOwnEntryLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryOwnEntryLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryOwnEntryLabel
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