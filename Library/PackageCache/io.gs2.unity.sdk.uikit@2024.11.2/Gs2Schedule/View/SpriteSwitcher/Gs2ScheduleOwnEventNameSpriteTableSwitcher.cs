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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Schedule.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Event/View/SpriteSwitcher/Properties/Name/Gs2ScheduleOwnEventNameSpriteTableSwitcher")]
    public partial class Gs2ScheduleOwnEventNameSpriteTableSwitcher : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this.sprites.Count(v => v.value == _fetcher.Event.Name) > 0) {
                this.onUpdate.Invoke(this.sprites.Find(v => v.value == _fetcher.Event.Name).sprite);
            }
            else {
                this.onUpdate.Invoke(this.defaultSprite);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleOwnEventNameSpriteTableSwitcher
    {
        private Gs2ScheduleOwnEventFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ScheduleOwnEventFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnEventFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleOwnEventFetcher.");
                enabled = false;
            }
            if (this.sprites == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ScheduleOwnEventFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnEventFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.sprites == null) {
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

    public partial class Gs2ScheduleOwnEventNameSpriteTableSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleOwnEventNameSpriteTableSwitcher
    {
        [System.Serializable]
        public class SpriteTableEntry
        {
            public string value;
            public Sprite sprite;
        }

        public List<SpriteTableEntry> sprites;
        public Sprite defaultSprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleOwnEventNameSpriteTableSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}