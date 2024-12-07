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
// ReSharper disable UnusedAutoPropertyAccessor.Local
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
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using UnityEngine;
using UnityEngine.Events;
using Ranking = Gs2.Unity.Gs2Ranking.ScriptableObject.Ranking;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    public partial class Gs2RankingRankingPutScoreAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onPutScoreStart.Invoke();

            
            var domain = clientHolder.Gs2.Ranking.Namespace(
                this._context.Ranking.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).RankingCategory(
                this._context.Ranking.CategoryName,
                this._context.Ranking.AdditionalScopeName
            );
            var future = domain.PutScoreFuture(
                Score,
                Metadata
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            this.onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onPutScoreComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onPutScoreComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {

        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingRankingPutScoreAction
    {
        private Gs2RankingRankingContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2RankingRankingContext>() ?? GetComponentInParent<Gs2RankingRankingContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingRankingContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2RankingRankingContext>() ?? GetComponentInParent<Gs2RankingRankingContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingRankingPutScoreAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2RankingRankingPutScoreAction
    {
        public bool WaitAsyncProcessComplete;
        public long Score;
        public string Metadata;

        public void SetScore(long value) {
            this.Score = value;
            this.onChangeScore.Invoke(this.Score);
            this.OnChange.Invoke();
        }

        public void DecreaseScore() {
            this.Score -= 1;
            this.onChangeScore.Invoke(this.Score);
            this.OnChange.Invoke();
        }

        public void IncreaseScore() {
            this.Score += 1;
            this.onChangeScore.Invoke(this.Score);
            this.OnChange.Invoke();
        }

        public void SetMetadata(string value) {
            this.Metadata = value;
            this.onChangeMetadata.Invoke(this.Metadata);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingRankingPutScoreAction
    {

        [Serializable]
        private class ChangeScoreEvent : UnityEvent<long>
        {

        }

        [SerializeField]
        private ChangeScoreEvent onChangeScore = new ChangeScoreEvent();
        public event UnityAction<long> OnChangeScore
        {
            add => this.onChangeScore.AddListener(value);
            remove => this.onChangeScore.RemoveListener(value);
        }

        [Serializable]
        private class ChangeMetadataEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeMetadataEvent onChangeMetadata = new ChangeMetadataEvent();
        public event UnityAction<string> OnChangeMetadata
        {
            add => this.onChangeMetadata.AddListener(value);
            remove => this.onChangeMetadata.RemoveListener(value);
        }

        [Serializable]
        private class PutScoreStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private PutScoreStartEvent onPutScoreStart = new PutScoreStartEvent();

        public event UnityAction OnPutScoreStart
        {
            add => this.onPutScoreStart.AddListener(value);
            remove => this.onPutScoreStart.RemoveListener(value);
        }

        [Serializable]
        private class PutScoreCompleteEvent : UnityEvent<EzScore>
        {

        }

        [SerializeField]
        private PutScoreCompleteEvent onPutScoreComplete = new PutScoreCompleteEvent();
        public event UnityAction<EzScore> OnPutScoreComplete
        {
            add => this.onPutScoreComplete.AddListener(value);
            remove => this.onPutScoreComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// Context Menu
    /// </summary>
    public partial class Gs2RankingRankingPutScoreAction
    {
        [MenuItem("GameObject/Game Server Services/Ranking/Ranking/Action/PutScore", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2RankingRankingPutScoreAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Ranking/Prefabs/Action/Gs2RankingRankingPutScoreAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}