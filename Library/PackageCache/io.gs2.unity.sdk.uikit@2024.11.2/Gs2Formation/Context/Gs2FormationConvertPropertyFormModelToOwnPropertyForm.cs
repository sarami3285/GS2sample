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
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    [AddComponentMenu("GS2 UIKit/Formation/PropertyForm/Context/Convert/Gs2FormationConvertPropertyFormModelToOwnPropertyForm")]
    public class Gs2FormationConvertPropertyFormModelToOwnPropertyForm : MonoBehaviour
    {
        private Gs2FormationPropertyFormModelContext _originalContext;
        private Gs2FormationOwnPropertyFormContext _context;

        public void Awake() {
            this._originalContext = GetComponent<Gs2FormationPropertyFormModelContext>() ?? GetComponentInParent<Gs2FormationPropertyFormModelContext>();
            if (this._originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationPropertyFormModelContext.");
                enabled = false;
            }
            this._context = GetComponent<Gs2FormationOwnPropertyFormContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnPropertyFormContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._originalContext = GetComponent<Gs2FormationPropertyFormModelContext>() ?? GetComponentInParent<Gs2FormationPropertyFormModelContext>();
            if (_originalContext == null) {
                return true;
            }
            this._context = GetComponent<Gs2FormationOwnPropertyFormContext>();
            if (this._context == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onUpdateContext;

        private void OnUpdateContext() {
            this._context.SetOwnPropertyForm(
                OwnPropertyForm.New(
                    _originalContext.PropertyFormModel.Namespace,
                    _originalContext.PropertyFormModel.propertyFormModelName,
                    ""
                )
            );
        }

        public void OnEnable() {
            _onUpdateContext = () =>
            {
                OnUpdateContext();
            };
            this._originalContext.OnUpdate.AddListener(this._onUpdateContext);
        }

        public void OnDisable() {
            if (this._onUpdateContext != null) {
                this._originalContext.OnUpdate.RemoveListener(this._onUpdateContext);
                this._onUpdateContext = null;
            }
        }
    }
}