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
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Schedule.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Schedule/Event/View/Localization/Gs2ScheduleEventLocalizationVariables")]
    public partial class Gs2ScheduleEventLocalizationVariables : MonoBehaviour
    {
        private void OnFetched()
        {
            this.target.StringReference["name"] = new StringVariable {
                Value = _fetcher?.Event?.Name ?? "",
            };
            this.target.StringReference["metadata"] = new StringVariable {
                Value = _fetcher?.Event?.Metadata ?? "",
            };
            this.target.StringReference["scheduleType"] = new StringVariable {
                Value = _fetcher?.Event?.ScheduleType ?? "",
            };
            this.target.StringReference["repeatType"] = new StringVariable {
                Value = _fetcher?.Event?.RepeatType ?? "",
            };
            this.target.StringReference["repeatBeginDayOfMonth"] = new IntVariable {
                Value = _fetcher?.Event?.RepeatBeginDayOfMonth ?? 0,
            };
            this.target.StringReference["repeatEndDayOfMonth"] = new IntVariable {
                Value = _fetcher?.Event?.RepeatEndDayOfMonth ?? 0,
            };
            this.target.StringReference["repeatBeginDayOfWeek"] = new StringVariable {
                Value = _fetcher?.Event?.RepeatBeginDayOfWeek ?? "",
            };
            this.target.StringReference["repeatEndDayOfWeek"] = new StringVariable {
                Value = _fetcher?.Event?.RepeatEndDayOfWeek ?? "",
            };
            this.target.StringReference["repeatBeginHour"] = new IntVariable {
                Value = _fetcher?.Event?.RepeatBeginHour ?? 0,
            };
            this.target.StringReference["repeatEndHour"] = new IntVariable {
                Value = _fetcher?.Event?.RepeatEndHour ?? 0,
            };
            this.target.StringReference["relativeTriggerName"] = new StringVariable {
                Value = _fetcher?.Event?.RelativeTriggerName ?? "",
            };
            this.target.enabled = true;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleEventLocalizationVariables
    {
        private Gs2ScheduleOwnEventFetcher _fetcher;

        public void Awake() {
            this.target.enabled = false;
            this._fetcher = GetComponent<Gs2ScheduleOwnEventFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnEventFetcher>();

            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleEventFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ScheduleOwnEventFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnEventFetcher>(true);
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

    public partial class Gs2ScheduleEventLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventLocalizationVariables
    {

    }
}

#endif