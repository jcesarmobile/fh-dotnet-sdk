﻿using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FHSDK;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using FHSDK.FHHttpClient;
using Newtonsoft.Json.Linq;

namespace FHSDKTest
{
    [TestClass]
    public class FHTest
    {
        [TestMethod]
        public async Task Read_App_Config()
        {
            AppProps appProps = await FH.GetAppProps();
            Assert.AreEqual("testAppid", appProps.appid, "AppId not match");
            Assert.AreEqual("testKey", appProps.appkey, "AppKey not match");
            Assert.AreEqual("test", appProps.mode, "Mode not match");
        }

        [TestMethod]
        public async Task FH_Init_Test()
        {
            bool success = await FH.Init();
            Assert.IsTrue(success); 
        }

        [TestMethod]
        public async Task FH_Act_Test()
        {
            bool success = await FH.Init();
            Assert.IsTrue(success);
            IDictionary<string, object> reqParams = new Dictionary<string, object>();
            reqParams.Add("data", "hello");
            FHResponse response = await FH.Act("echo", reqParams);
            JObject resJson = response.GetResponseAsJObject();
            Assert.IsNull(response.Error);
            Assert.AreEqual("hello", resJson["request"]["data"]);
        }
    }
}