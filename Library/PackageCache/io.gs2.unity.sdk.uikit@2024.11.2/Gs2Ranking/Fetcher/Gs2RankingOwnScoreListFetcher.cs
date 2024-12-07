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
 *
 * deny overwrite
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Ranking.Domain.Model;
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Ranking.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/Score/Fetcher/Gs2RankingOwnScoreListFetcher")]
    public partial class Gs2RankingOwnScoreListFetcher : MonoBehaviour
    {
        private EzUserGameSessionDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Load() {
            var retryWaitSecond = 1;
            var it = _domain.Scores(
                this.Context.CategoryModel.CategoryName,
                this._userContext.User.UserId
            );
            var items = new List<Gs2.Unity.Gs2Ranking.Model.EzScore>();
            while (it.HasNext())
            {
                yield return it.Next();
                if (it.Error != null)
                {
                    if (it.Error is BadRequestException || it.Error is NotFoundException)
                    {
                        onError.Invoke(it.Error, null);
                        Debug.LogError($"{gameObject.GetFullPath()}: {it.Error.Message}");
                        break;
                    }
                    else {
                        onError.Invoke(new CanIgnoreException(it.Error), null);
                    }
                    yield return new WaitForSeconds(retryWaitSecond);
                    retryWaitSecond *= 2;
                }
                else {
                    if (it.Current != null)
                    {
                        items.Add(it.Current);
                    } else {
                        break;
                    }
                }
            }

            retryWaitSecond = 1;
            Scores = items;
            Fetched = true;

            this.OnFetched.Invoke();
        }

        private IEnumerator Fetch()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && Context.CategoryModel != null);

            this._domain = clientHolder.Gs2.Ranking.Namespace(
                this.Context.CategoryModel.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            this._callbackId = this._domain.SubscribeScores(
                items =>
                {
                    Scores = items.ToList();
                    this.OnFetched.Invoke();
                },
                this.Context.CategoryModel.CategoryName,
                gameSessionHolder.GameSession.UserId
            );

            yield return Load();
        }

        public void OnUpdateContext() {
            OnDisable();
            Awake();
            OnEnable();
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
            Context.OnUpdate.AddListener(OnUpdateContext);
        }

        public void OnDisable()
        {
            Context.OnUpdate.RemoveListener(OnUpdateContext);

            if (this._domain == null) {
                return;
            }
            if (!this._callbackId.HasValue) {
                return;
            }
            var gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._domain.UnsubscribeScores(
                this._callbackId.Value,
                this.Context.CategoryModel.CategoryName,
                gameSessionHolder.GameSession.UserId
            );
            this._callbackId = null;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingOwnScoreListFetcher
    {
        public Gs2RankingCategoryModelContext Context;
        private Gs2RankingUserContext _userContext;

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2RankingCategoryModelContext>() ?? GetComponentInParent<Gs2RankingCategoryModelContext>();
            _userContext = GetComponent<Gs2RankingUserContext>() ?? GetComponentInParent<Gs2RankingUserContext>();

            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingCategoryModelContext.");
                enabled = false;
            }
            if (_userContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingUserContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2RankingCategoryModelContext>() ?? GetComponentInParent<Gs2RankingCategoryModelContext>(true);
            if (Context == null) {
                return true;
            }
            _userContext = GetComponent<Gs2RankingUserContext>() ?? GetComponentInParent<Gs2RankingUserContext>();
            if (_userContext == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2RankingOwnScoreListFetcher
    {
        public List<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2RankingOwnScoreListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingOwnScoreListFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}