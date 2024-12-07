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
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Matchmaking/Rating/View/Label/Gs2MatchmakingOwnRatingLabel")]
    public partial class Gs2MatchmakingOwnRatingLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var createdAt = this._fetcher.Rating.CreatedAt == null ? DateTime.Now : _fetcher.Rating.CreatedAt.ToLocalTime();
            var updatedAt = this._fetcher.Rating.UpdatedAt == null ? DateTime.Now : _fetcher.Rating.UpdatedAt.ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{ratingId}", $"{this._fetcher?.Rating?.RatingId}"
                ).Replace(
                    "{name}", $"{this._fetcher?.Rating?.Name}"
                ).Replace(
                    "{userId}", $"{this._fetcher?.Rating?.UserId}"
                ).Replace(
                    "{rateValue}", $"{this._fetcher?.Rating?.RateValue}"
                ).Replace(
                    "{createdAt:yyyy}", createdAt.ToString("yyyy")
                ).Replace(
                    "{createdAt:yy}", createdAt.ToString("yy")
                ).Replace(
                    "{createdAt:MM}", createdAt.ToString("MM")
                ).Replace(
                    "{createdAt:MMM}", createdAt.ToString("MMM")
                ).Replace(
                    "{createdAt:dd}", createdAt.ToString("dd")
                ).Replace(
                    "{createdAt:hh}", createdAt.ToString("hh")
                ).Replace(
                    "{createdAt:HH}", createdAt.ToString("HH")
                ).Replace(
                    "{createdAt:tt}", createdAt.ToString("tt")
                ).Replace(
                    "{createdAt:mm}", createdAt.ToString("mm")
                ).Replace(
                    "{createdAt:ss}", createdAt.ToString("ss")
                ).Replace(
                    "{updatedAt:yyyy}", updatedAt.ToString("yyyy")
                ).Replace(
                    "{updatedAt:yy}", updatedAt.ToString("yy")
                ).Replace(
                    "{updatedAt:MM}", updatedAt.ToString("MM")
                ).Replace(
                    "{updatedAt:MMM}", updatedAt.ToString("MMM")
                ).Replace(
                    "{updatedAt:dd}", updatedAt.ToString("dd")
                ).Replace(
                    "{updatedAt:hh}", updatedAt.ToString("hh")
                ).Replace(
                    "{updatedAt:HH}", updatedAt.ToString("HH")
                ).Replace(
                    "{updatedAt:tt}", updatedAt.ToString("tt")
                ).Replace(
                    "{updatedAt:mm}", updatedAt.ToString("mm")
                ).Replace(
                    "{updatedAt:ss}", updatedAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MatchmakingOwnRatingLabel
    {
        private Gs2MatchmakingOwnRatingFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MatchmakingOwnRatingFetcher>() ?? GetComponentInParent<Gs2MatchmakingOwnRatingFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MatchmakingOwnRatingFetcher>() ?? GetComponentInParent<Gs2MatchmakingOwnRatingFetcher>(true);
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

    public partial class Gs2MatchmakingOwnRatingLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingOwnRatingLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingOwnRatingLabel
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