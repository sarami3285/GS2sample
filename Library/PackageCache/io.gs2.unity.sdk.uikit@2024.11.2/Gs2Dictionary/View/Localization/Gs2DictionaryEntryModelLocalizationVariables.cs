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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/EntryModel/View/Localization/Gs2DictionaryEntryModelLocalizationVariables")]
    public partial class Gs2DictionaryEntryModelLocalizationVariables : MonoBehaviour
    {
        private void OnFetched()
        {
            this.target.StringReference["name"] = new StringVariable {
                Value = _fetcher?.EntryModel?.Name ?? "",
            };
            this.target.StringReference["metadata"] = new StringVariable {
                Value = _fetcher?.EntryModel?.Metadata ?? "",
            };
            this.target.enabled = true;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryEntryModelLocalizationVariables
    {
        private Gs2DictionaryEntryModelFetcher _fetcher;

        public void Awake() {
            this.target.enabled = false;
            this._fetcher = GetComponent<Gs2DictionaryEntryModelFetcher>() ?? GetComponentInParent<Gs2DictionaryEntryModelFetcher>();

            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryEntryModelFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2DictionaryEntryModelFetcher>() ?? GetComponentInParent<Gs2DictionaryEntryModelFetcher>(true);
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

    public partial class Gs2DictionaryEntryModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryEntryModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryModelLocalizationVariables
    {

    }
}

#endif