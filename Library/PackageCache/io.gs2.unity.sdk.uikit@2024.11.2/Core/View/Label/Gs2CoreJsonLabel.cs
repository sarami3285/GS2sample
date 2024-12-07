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
using System.Linq;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/View/Label/Gs2CoreJsonLabel")]
    public partial class Gs2CoreJsonLabel : MonoBehaviour
    {
        public void OnChangeValue() {
            var value = this.path.Aggregate(JsonMapper.ToObject(this.value), (current, element) => current[element]);
            onUpdate?.Invoke(value.ToString());
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreJsonLabel
    {
        public void Awake()
        {
            OnChangeValue();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreJsonLabel
    {
        public void SetPath(string[] path) {
            this.path = path;
            OnChangeValue();
        }
        public void SetValue(string value) {
            this.value = value;
            OnChangeValue();
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreJsonLabel
    {
        public string[] path;
        public string value;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreJsonLabel
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