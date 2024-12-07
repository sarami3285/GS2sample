using System;
using System.Collections.Generic;
using Gs2.Gs2Inventory.Request;
using Gs2.Util.LitJson;

namespace Gs2.Unity.UiKit.Core.Model
{
    public static class ListAcquireActionExt {
        public static List<Gs2.Unity.Core.Model.EzAcquireAction> Denormalize(this List<Gs2.Unity.Core.Model.EzAcquireAction> self) {
            var items = new List<Gs2.Unity.Core.Model.EzAcquireAction>();
            foreach (var consumeAction in self) {
                if (consumeAction.Action.IndexOf(":Verify", StringComparison.Ordinal) == -1) {
                    continue;
                }
                if (consumeAction.Action == "Gs2Inventory:AcquireSimpleItemsByUserId") {
                    var request = AcquireSimpleItemsByUserIdRequest.FromJson(JsonMapper.ToObject(consumeAction.Request));
                    foreach (var consumeCount in request.AcquireCounts) {
                        request.AcquireCounts = new[] {consumeCount};
                        items.Add(new Gs2.Unity.Core.Model.EzAcquireAction {
                            Action = consumeAction.Action,
                            Request = request.ToJson().ToJson(),
                        });
                    }
                }
                else {
                    items.Add(consumeAction);
                }
            }
            return items;
        } 
    }
}
