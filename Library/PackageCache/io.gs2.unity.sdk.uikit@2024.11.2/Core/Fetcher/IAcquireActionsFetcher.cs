using System.Collections.Generic;
using Gs2.Unity.Core.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Core.Fetcher
{
    public interface IAcquireActionsFetcher
    {
        List<EzAcquireAction> AcquireActions(string context = "default");

        bool IsFetched();
        
        UnityEvent OnFetchedEvent();

        GameObject GameObject();
    }
}
