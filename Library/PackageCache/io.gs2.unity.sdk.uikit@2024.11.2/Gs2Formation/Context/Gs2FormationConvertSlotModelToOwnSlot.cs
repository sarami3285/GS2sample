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
    [AddComponentMenu("GS2 UIKit/Formation/Slot/Context/Convert/Gs2FormationConvertSlotModelToOwnSlot")]
    public class Gs2FormationConvertSlotModelToOwnSlot : MonoBehaviour
    {
        private Gs2FormationSlotModelContext _originalContext;
        private Gs2FormationOwnSlotContext _context;

        public void Awake() {
            this._originalContext = GetComponent<Gs2FormationSlotModelContext>() ?? GetComponentInParent<Gs2FormationSlotModelContext>();
            if (this._originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationSlotModelContext.");
                enabled = false;
            }
            this._context = GetComponent<Gs2FormationOwnSlotContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnSlotContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._originalContext = GetComponent<Gs2FormationSlotModelContext>() ?? GetComponentInParent<Gs2FormationSlotModelContext>();
            if (this._originalContext == null) {
                return true;
            }
            this._context = GetComponent<Gs2FormationOwnSlotContext>();
            if (this._context == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onUpdateContext;

        private void OnUpdateContext() {
            var ownFormIndex = 0;
            var ownFormContext = GetComponent<Gs2FormationOwnFormContext>() ?? GetComponentInParent<Gs2FormationOwnFormContext>();
            if (ownFormContext != null) {
                ownFormIndex = ownFormContext.Form.Index;
            }
            this._context.SetOwnSlot(
                OwnSlot.New(
                    OwnForm.New(
                        OwnMold.New(
                            this._originalContext.SlotModel.FormModel.MoldModel.Namespace,
                            this._originalContext.SlotModel.FormModel.MoldModelName
                        ),
                        ownFormIndex
                    ),
                    this._originalContext.SlotModel.SlotModelName
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