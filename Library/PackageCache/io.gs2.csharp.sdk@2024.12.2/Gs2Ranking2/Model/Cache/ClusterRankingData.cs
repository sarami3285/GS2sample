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

// ReSharper disable ConvertSwitchStatementToSwitchExpression

#pragma warning disable CS1522 // Empty switch block

using System;
using Gs2.Core.Domain;
using Gs2.Core.Net;
using Gs2.Core.Util;
#if UNITY_2017_1_OR_NEWER
using System.Collections;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
    #endif
#else
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Ranking2.Model.Cache
{
    public static partial class ClusterRankingDataExt
    {
        public static string CacheParentKey(
            this ClusterRankingData self,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season
        ) {
            return string.Join(
                ":",
                "ranking2",
                namespaceName,
                rankingName,
                clusterName,
                season.ToString(),
                "ClusterRankingData"
            );
        }

        public static string CacheKey(
            this ClusterRankingData self,
            string userId
        ) {
            return string.Join(
                ":",
                userId
            );
        }

#if UNITY_2017_1_OR_NEWER
        public static IFuture<ClusterRankingData> FetchFuture(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            string userId,
            Func<IFuture<ClusterRankingData>> fetchImpl
        ) {
            IEnumerator Impl(IFuture<ClusterRankingData> self)
            {
                var future = fetchImpl();
                yield return future;
                if (future.Error != null)
                {
                    if (future.Error is Gs2.Core.Exception.NotFoundException e)
                    {
                        (null as ClusterRankingData).PutCache(
                            cache,
                            namespaceName,
                            rankingName,
                            clusterName,
                            season,
                            userId
                        );
                        if (e.Errors.Length != 0 && e.Errors[0].Component == "clusterRankingData") {
                            self.OnComplete(default);
                            yield break;
                        }
                    }
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                item.PutCache(
                    cache,
                    namespaceName,
                    rankingName,
                    clusterName,
                    season,
                    userId
                );
                self.OnComplete(item);
            }
            return new Gs2InlineFuture<ClusterRankingData>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public static async UniTask<ClusterRankingData> FetchAsync(
    #else
        public static async Task<ClusterRankingData> FetchAsync(
    #endif
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            string userId,
    #if UNITY_2017_1_OR_NEWER
            Func<UniTask<ClusterRankingData>> fetchImpl
    #else
            Func<Task<ClusterRankingData>> fetchImpl
    #endif
        ) {
            using (await cache.GetLockObject<ClusterRankingData>(
                       self.CacheParentKey(
                            namespaceName,
                            rankingName,
                            clusterName,
                            season
                       ),
                       self.CacheKey(
                            userId
                       )
                   ).LockAsync()) {
                try {
                    var item = await fetchImpl();
                    item.PutCache(
                        cache,
                        namespaceName,
                        rankingName,
                        clusterName,
                        season,
                        userId
                    );
                    return item;
                }
                catch (Gs2.Core.Exception.NotFoundException e) {
                    (null as ClusterRankingData).PutCache(
                        cache,
                        namespaceName,
                        rankingName,
                        clusterName,
                        season,
                        userId
                    );
                    if (e.errors.Length == 0 || e.errors[0].component != "clusterRankingData") {
                        throw;
                    }
                    return null;
                }
            }
        }
#endif

        public static Tuple<ClusterRankingData, bool> GetCache(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            string userId
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            return cache.Get<ClusterRankingData>(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                self.CacheKey(
                    userId
                )
            );
        }

        public static void PutCache(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            string userId
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            var (value, find) = cache.Get<ClusterRankingData>(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                self.CacheKey(
                    userId
                )
            );
            if (find && (value?.Revision ?? 0) > (self?.Revision ?? 0) && (self?.Revision ?? 0) > 1) {
                return;
            }
            cache.Put(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                self.CacheKey(
                    userId
                ),
                self,
                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
            );
        }

        public static void DeleteCache(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            string userId
        ) {
            if (userId == null) {
                throw new NullReferenceException();
            }
            cache.Delete<ClusterRankingData>(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                self.CacheKey(
                    userId
                )
            );
        }

        public static void ListSubscribe(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            Action<ClusterRankingData[]> callback
        ) {
            cache.ListSubscribe<ClusterRankingData>(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                callback
            );
        }

        public static void ListUnsubscribe(
            this ClusterRankingData self,
            CacheDatabase cache,
            string namespaceName,
            string rankingName,
            string clusterName,
            long? season,
            ulong callbackId
        ) {
            cache.ListUnsubscribe<ClusterRankingData>(
                self.CacheParentKey(
                    namespaceName,
                    rankingName,
                    clusterName,
                    season
                ),
                callbackId
            );
        }
    }
}