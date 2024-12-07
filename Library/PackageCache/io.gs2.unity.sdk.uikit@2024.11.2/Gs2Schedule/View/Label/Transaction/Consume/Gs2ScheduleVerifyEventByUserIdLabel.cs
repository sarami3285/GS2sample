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
using Gs2.Gs2Schedule.Request;
using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Schedule.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Event/View/Label/Transaction/Gs2ScheduleVerifyEventByUserIdLabel")]
    public partial class Gs2ScheduleVerifyEventByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2ScheduleOwnEventContext>();
                context.SetOwnEvent(
                    OwnEvent.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.EventName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2ScheduleOwnEventFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Event == null) {
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
                    "{eventName}",
                    $"{this._fetcher.Request.EventName}"
                ).Replace(
                    "{verifyType}",
                    $"{this._fetcher.Request.VerifyType}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.Event.Name}"
                ).Replace(
                    "{userData:metadata}",
                    $"{this._userDataFetcher.Event.Metadata}"
                ).Replace(
                    "{userData:scheduleType}",
                    $"{this._userDataFetcher.Event.ScheduleType}"
                ).Replace(
                    "{userData:repeatType}",
                    $"{this._userDataFetcher.Event.RepeatType}"
                ).Replace(
                    "{userData:absoluteBegin}",
                    $"{this._userDataFetcher.Event.AbsoluteBegin}"
                ).Replace(
                    "{userData:absoluteEnd}",
                    $"{this._userDataFetcher.Event.AbsoluteEnd}"
                ).Replace(
                    "{userData:repeatBeginDayOfMonth}",
                    $"{this._userDataFetcher.Event.RepeatBeginDayOfMonth}"
                ).Replace(
                    "{userData:repeatEndDayOfMonth}",
                    $"{this._userDataFetcher.Event.RepeatEndDayOfMonth}"
                ).Replace(
                    "{userData:repeatBeginDayOfWeek}",
                    $"{this._userDataFetcher.Event.RepeatBeginDayOfWeek}"
                ).Replace(
                    "{userData:repeatEndDayOfWeek}",
                    $"{this._userDataFetcher.Event.RepeatEndDayOfWeek}"
                ).Replace(
                    "{userData:repeatBeginHour}",
                    $"{this._userDataFetcher.Event.RepeatBeginHour}"
                ).Replace(
                    "{userData:repeatEndHour}",
                    $"{this._userDataFetcher.Event.RepeatEndHour}"
                ).Replace(
                    "{userData:relativeTriggerName}",
                    $"{this._userDataFetcher.Event.RelativeTriggerName}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleVerifyEventByUserIdLabel
    {
        private Gs2ScheduleVerifyEventByUserIdFetcher _fetcher;
        private Gs2ScheduleOwnEventFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ScheduleVerifyEventByUserIdFetcher>() ?? GetComponentInParent<Gs2ScheduleVerifyEventByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleVerifyEventByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2ScheduleOwnEventFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnEventFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ScheduleVerifyEventByUserIdFetcher>() ?? GetComponentInParent<Gs2ScheduleVerifyEventByUserIdFetcher>(true);
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

    public partial class Gs2ScheduleVerifyEventByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleVerifyEventByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleVerifyEventByUserIdLabel
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