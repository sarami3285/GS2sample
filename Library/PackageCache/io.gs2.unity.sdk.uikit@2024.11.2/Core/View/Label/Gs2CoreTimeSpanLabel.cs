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

using System;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/View/Label/Gs2CoreTimeSpanLabel")]
    public partial class Gs2CoreTimeSpanLabel : MonoBehaviour
    {
        public void OnChangeValue()
        {
            if (time > 0)
            {
                var timestamp = UnixTime.FromUnixTime(time).ToLocalTime();
                var timeSpan = DateTime.Now - timestamp;

                var hours = timeSpan.ToString("hh");
                var days = "0";
                if (hours.IndexOf(".", StringComparison.Ordinal) != -1) {
                    hours = hours.Substring(hours.IndexOf(".", StringComparison.Ordinal) + 1);
                    days = hours.Substring(0, hours.IndexOf(".", StringComparison.Ordinal));
                }
                onUpdate?.Invoke(
                    format.Replace(
                        "{days}", days
                    ).Replace(
                        "{hours}", hours
                    ).Replace(
                        "{minutes}", timeSpan.ToString("mm")
                    ).Replace(
                        "{seconds}", timeSpan.ToString("ss")
                    )
                );
            }
            else {
                onUpdate?.Invoke("");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreTimeSpanLabel
    {
        public void Awake()
        {
            OnChangeValue();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreTimeSpanLabel
    {
        public void SetFormat(string format) {
            this.format = format;
            OnChangeValue();
        }
        public void SetTime(long time) {
            this.time = time;
            OnChangeValue();
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreTimeSpanLabel
    {
        public string format;
        public long time;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreTimeSpanLabel
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