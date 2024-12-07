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
using Gs2.Unity.Gs2Matchmaking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using UnityEngine;
using UnityEngine.Events;
using Vote = Gs2.Unity.Gs2Matchmaking.ScriptableObject.Vote;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    public partial class Gs2MatchmakingVoteVoteAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);

            this.onVoteStart.Invoke();

            
            var domain = clientHolder.Gs2.Matchmaking.Namespace(
                this._context.Vote.NamespaceName
            );
            var future = domain.VoteFuture(
                BallotBody,
                BallotSignature,
                GameResults.ToArray(),
                KeyId
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

                        this.onVoteComplete.Invoke(future3.Result);
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

            this.onVoteComplete.Invoke(future2.Result);
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

    public partial class Gs2MatchmakingVoteVoteAction
    {
        private Gs2MatchmakingVoteContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2MatchmakingVoteContext>() ?? GetComponentInParent<Gs2MatchmakingVoteContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingVoteContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2MatchmakingVoteContext>() ?? GetComponentInParent<Gs2MatchmakingVoteContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingVoteVoteAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MatchmakingVoteVoteAction
    {
        public bool WaitAsyncProcessComplete;
        public string BallotBody;
        public string BallotSignature;
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> GameResults;
        public string KeyId;

        public void SetBallotBody(string value) {
            this.BallotBody = value;
            this.onChangeBallotBody.Invoke(this.BallotBody);
            this.OnChange.Invoke();
        }

        public void SetBallotSignature(string value) {
            this.BallotSignature = value;
            this.onChangeBallotSignature.Invoke(this.BallotSignature);
            this.OnChange.Invoke();
        }

        public void SetGameResults(List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> value) {
            this.GameResults = value;
            this.onChangeGameResults.Invoke(this.GameResults);
            this.OnChange.Invoke();
        }

        public void SetKeyId(string value) {
            this.KeyId = value;
            this.onChangeKeyId.Invoke(this.KeyId);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingVoteVoteAction
    {

        [Serializable]
        private class ChangeBallotBodyEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeBallotBodyEvent onChangeBallotBody = new ChangeBallotBodyEvent();
        public event UnityAction<string> OnChangeBallotBody
        {
            add => this.onChangeBallotBody.AddListener(value);
            remove => this.onChangeBallotBody.RemoveListener(value);
        }

        [Serializable]
        private class ChangeBallotSignatureEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeBallotSignatureEvent onChangeBallotSignature = new ChangeBallotSignatureEvent();
        public event UnityAction<string> OnChangeBallotSignature
        {
            add => this.onChangeBallotSignature.AddListener(value);
            remove => this.onChangeBallotSignature.RemoveListener(value);
        }

        [Serializable]
        private class ChangeGameResultsEvent : UnityEvent<List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult>>
        {

        }

        [SerializeField]
        private ChangeGameResultsEvent onChangeGameResults = new ChangeGameResultsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult>> OnChangeGameResults
        {
            add => this.onChangeGameResults.AddListener(value);
            remove => this.onChangeGameResults.RemoveListener(value);
        }

        [Serializable]
        private class ChangeKeyIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeKeyIdEvent onChangeKeyId = new ChangeKeyIdEvent();
        public event UnityAction<string> OnChangeKeyId
        {
            add => this.onChangeKeyId.AddListener(value);
            remove => this.onChangeKeyId.RemoveListener(value);
        }

        [Serializable]
        private class VoteStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private VoteStartEvent onVoteStart = new VoteStartEvent();

        public event UnityAction OnVoteStart
        {
            add => this.onVoteStart.AddListener(value);
            remove => this.onVoteStart.RemoveListener(value);
        }

        [Serializable]
        private class VoteCompleteEvent : UnityEvent<EzSignedBallot>
        {

        }

        [SerializeField]
        private VoteCompleteEvent onVoteComplete = new VoteCompleteEvent();
        public event UnityAction<EzSignedBallot> OnVoteComplete
        {
            add => this.onVoteComplete.AddListener(value);
            remove => this.onVoteComplete.RemoveListener(value);
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
    public partial class Gs2MatchmakingVoteVoteAction
    {
        [MenuItem("GameObject/Game Server Services/Matchmaking/Vote/Action/Vote", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MatchmakingVoteVoteAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Matchmaking/Prefabs/Action/Gs2MatchmakingVoteVoteAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}