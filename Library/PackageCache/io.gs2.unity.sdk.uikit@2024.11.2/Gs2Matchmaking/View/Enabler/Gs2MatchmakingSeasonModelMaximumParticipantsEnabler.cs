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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Matchmaking/SeasonModel/View/Enabler/Properties/MaximumParticipants/Gs2MatchmakingSeasonModelMaximumParticipantsEnabler")]
    public partial class Gs2MatchmakingSeasonModelMaximumParticipantsEnabler : MonoBehaviour
    {
        private void OnFetched()
        {
            switch(this.expression)
            {
                case Expression.In:
                    this.target.SetActive(this.enableMaximumParticipantses.Contains(this._fetcher.SeasonModel.MaximumParticipants));
                    break;
                case Expression.NotIn:
                    this.target.SetActive(!this.enableMaximumParticipantses.Contains(this._fetcher.SeasonModel.MaximumParticipants));
                    break;
                case Expression.Less:
                    this.target.SetActive(this.enableMaximumParticipants > this._fetcher.SeasonModel.MaximumParticipants);
                    break;
                case Expression.LessEqual:
                    this.target.SetActive(this.enableMaximumParticipants >= this._fetcher.SeasonModel.MaximumParticipants);
                    break;
                case Expression.Greater:
                    this.target.SetActive(this.enableMaximumParticipants < this._fetcher.SeasonModel.MaximumParticipants);
                    break;
                case Expression.GreaterEqual:
                    this.target.SetActive(this.enableMaximumParticipants <= this._fetcher.SeasonModel.MaximumParticipants);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MatchmakingSeasonModelMaximumParticipantsEnabler
    {
        private Gs2MatchmakingSeasonModelFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MatchmakingSeasonModelFetcher>() ?? GetComponentInParent<Gs2MatchmakingSeasonModelFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingSeasonModelFetcher.");
                enabled = false;
            }
            if (this.target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MatchmakingSeasonModelFetcher>() ?? GetComponentInParent<Gs2MatchmakingSeasonModelFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.target == null) {
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

    public partial class Gs2MatchmakingSeasonModelMaximumParticipantsEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingSeasonModelMaximumParticipantsEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableMaximumParticipantses;

        public int enableMaximumParticipants;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingSeasonModelMaximumParticipantsEnabler
    {
        
    }
}