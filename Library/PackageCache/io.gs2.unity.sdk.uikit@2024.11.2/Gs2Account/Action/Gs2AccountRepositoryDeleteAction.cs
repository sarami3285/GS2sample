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

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using Gs2ClientHolder = Gs2.Unity.Util.Gs2ClientHolder;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/AccountRepository/Action/Delete")]
    public partial class Gs2AccountRepositoryDeleteAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return null;
            try
            {
                _accountRepository.Delete();
                onDeleteComplete.Invoke();
                
                Gs2ClientHolder.Instance.Gs2.ClearCache();
            }
            catch (Gs2Exception e)
            {
                onError.Invoke(e, null);
            }
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
    
    public partial class Gs2AccountRepositoryDeleteAction
    {
        protected AbstractAccountRepository _accountRepository;

        public void Awake()
        {
            _accountRepository = GetComponentInParent<AbstractAccountRepository>() ?? GetComponent<AbstractAccountRepository>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountRepositoryDeleteAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountRepositoryDeleteAction
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountRepositoryDeleteAction
    {
        [Serializable]
        private class DeleteCompleteEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private DeleteCompleteEvent onDeleteComplete = new DeleteCompleteEvent();
        
        public event UnityAction OnDeleteComplete
        {
            add => onDeleteComplete.AddListener(value);
            remove => onDeleteComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}