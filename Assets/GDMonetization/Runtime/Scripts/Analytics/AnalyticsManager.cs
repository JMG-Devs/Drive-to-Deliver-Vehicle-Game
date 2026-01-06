using System;
using System.Collections;
using System.Collections.Generic;
using Monetization.Runtime.Ads;
using Monetization.Runtime.Analytics;
using UnityEngine;
using GameAnalyticsSDK;

namespace Monetization.Runtime.Analytics
{
    public static class AnalyticsManager
    {
        public static bool Initialized { get; private set; } = false;
        private static IAnalyticsInitialization[] InitializableNetworks;
        private static IAnalyticsEventsService[] EventsNetworks;
        private static IAnalyticsAdRevenueService[] RevenueNetworks;
        private static IMarketingServices[] MarketingNetworks;

        public static void Initialize()
        {
            AdjustAnalyticsNetwork adjust = new AdjustAnalyticsNetwork();
            FirebaseAnalyticsNetwork firebase = new FirebaseAnalyticsNetwork();
            AppMetricaAnalyticsNetwork appmetrica = new AppMetricaAnalyticsNetwork();
            FirebaseTaichiCampaign taichiCampaign = new FirebaseTaichiCampaign();

            InitializableNetworks = new IAnalyticsInitialization[]
            {
                firebase,
                adjust
            };

            EventsNetworks = new IAnalyticsEventsService[]
            {
                appmetrica,
                firebase
            };

            RevenueNetworks = new IAnalyticsAdRevenueService[]
            {
                firebase,
                taichiCampaign,
                adjust,
                appmetrica
            };

            MarketingNetworks = new IMarketingServices[]
            {
                firebase,
                appmetrica
            };

            for (int i = 0; i < InitializableNetworks.Length; i++)
            {
                InitializableNetworks[i].Initialize();
            }

            Initialized = true;
        }


        public static void LogLevelStart(int levelNumber)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Start_Level_" + levelNumber.ToString());
            var parameter = new Dictionary<string, object>();
            parameter.Add("Start_Level_", levelNumber);
            SendEvent("Progression_Event_", parameter);
            // FirebaseAnalytics.LogEvent("Start_Level_" + levelNumber.ToString());

        }

        public static void LogLevelCompleteSuccessful(int levelNumber)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Complete_Level_" + levelNumber.ToString());
            var parameter = new Dictionary<string, object>();
            parameter.Add("Complete_Level_", levelNumber);
            SendEvent("Progression_Event_", parameter);
            //  FirebaseAnalytics.LogEvent("Complete_Level_" + levelNumber.ToString());

        }


        public static void LogLevelCompleteFailed(int levelNumber)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Failed_Level_" + levelNumber.ToString());

            var parameter = new Dictionary<string, object>();
            parameter.Add("Failed_Level_", levelNumber);
            SendEvent("Progression_Event_", parameter);
            // FirebaseAnalytics.LogEvent("Failed_Level_" + levelNumber.ToString());

        }

        public static void LogRandomEvent(int levelNumber,string reason)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, reason + levelNumber.ToString());

            var parameter = new Dictionary<string, object>();
            parameter.Add(reason, levelNumber);
            SendEvent("Progression_Event_", parameter);
            // FirebaseAnalytics.LogEvent("Failed_Level_" + levelNumber.ToString());

        }








        /// <summary>
        /// Send event to firebase and appmetrica
        /// </summary>
        /// <param name="eventName">Must consist of letters, digits or _ (underscores)</param>
        public static void SendEvent(string eventName)
        {
            if (Initialized)
            {
                for (int index = 0; index < EventsNetworks.Length; index++)
                {
                    IAnalyticsEventsService analyticNetwork = EventsNetworks[index];
                    analyticNetwork.SendEvent(eventName);
                }
            }
        }

        /// <summary>
        /// Send event to firebase and appmetrica
        /// </summary>
        /// <param name="eventName">Must consist of letters, digits or _ (underscores)</param>
        /// <param name="parameters">Dictionary keys must consist of letters, digits or _ (underscores)</param>
        public static void SendEvent(string eventName, Dictionary<string, object> parameters)
        {
            if (Initialized)
            {
                for (int index = 0; index < EventsNetworks.Length; index++)
                {
                    IAnalyticsEventsService analyticNetwork = EventsNetworks[index];
                    if (parameters == null)
                    {
                        analyticNetwork.SendEvent(eventName);
                    }
                    else
                    {
                        analyticNetwork.SendEvent(eventName, parameters);
                    }
                }
            }
        }

        [Obsolete("Use SendEvent instead", true)]
        public static void SendAppMetricaEvent(string eventName, params string[] parameters)
        {
            if (Initialized)
            {
                for (int index = 0; index < EventsNetworks.Length; index++)
                {
                    IAnalyticsEventsService analyticNetwork = EventsNetworks[index];
                    if (analyticNetwork is AppMetricaAnalyticsNetwork appMetricaAnalyticNetwork)
                    {
                        appMetricaAnalyticNetwork.SendEvent(eventName, parameters);
                        break;
                    }
                }
            }
        }

        [Obsolete("Use SendEvent instead", true)]
        public static void SendAppMetricaEvent(string eventName, string json)
        {
            if (Initialized)
            {
                for (int index = 0; index < EventsNetworks.Length; index++)
                {
                    IAnalyticsEventsService analyticNetwork = EventsNetworks[index];
                    if (analyticNetwork is AppMetricaAnalyticsNetwork appMetricaAnalyticNetwork)
                    {
                        appMetricaAnalyticNetwork.SendEvent(eventName, json);
                        break;
                    }
                }
            }
        }

        public static void ReportAdRevenue(AdRevenueInfo adRevenueInfo)
        {
            if (Initialized)
            {
                for (int index = 0; index < RevenueNetworks.Length; index++)
                {
                    IAnalyticsAdRevenueService revenueNetwork = RevenueNetworks[index];
                    revenueNetwork.ReportAdRevenue(adRevenueInfo);
                }
            }
        }

//        public static void ReportInAppRevenue(InAppRevenueInfo inAppRevenueInfo)
//        {
//#if UNITY_EDITOR
//            return;
//#endif
//            if (Initialized)
//            {
//                for (int index = 0; index < MarketingNetworks.Length; index++)
//                {
//                    IMarketingServices revenueNetwork = MarketingNetworks[index];
//                    revenueNetwork.SendInAppPurchaseEvent(inAppRevenueInfo);
//                }
//            }
//        }

        public static bool IsNetworkInitialized<T>() where T : IAnalyticsInitialization
        {
            if (Initialized)
            {
                for (int index = 0; index < InitializableNetworks.Length; index++)
                {
                    var analyticNetwork = InitializableNetworks[index];
                    if (analyticNetwork is T output)
                    {
                        return output.IsInitialized;
                    }
                }
            }

            return false;
        }

        public static bool GetAnalyticsService<T>(out T result) where T : IAnalyticsEventsService
        {
            if (Initialized)
            {
                for (int index = 0; index < InitializableNetworks.Length; index++)
                {
                    var analyticNetwork = InitializableNetworks[index];
                    if (analyticNetwork is T network)
                    {
                        result = network;
                        return true;
                    }
                }
            }

            result = default(T);
            return false;
        }
    }
}