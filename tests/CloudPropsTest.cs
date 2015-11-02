﻿using FHSDK;
using FHSDK.Services;
using FHSDK.Services.Device;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using tests.Mocks;
using Xunit;

namespace tests
{
    public class CloudPropsTest
    {
        [Fact]
        public async Task TestReadCloudPropsWithJsonContainingValues()
        {
            // given
            var json = JObject.Parse(@"{
                'url': 'URL',
                'hosts': {'host': 'HOST',  'environment': 'ENV'}
             }");

            var props = new CloudProps(json);

            // when
            string host = props.GetCloudHost();
            string env = props.GetEnv();

            // then
            Assert.Equal("URL", host);
            Assert.Equal("ENV", env);
        }

        [Fact]
        public async Task TestURLFormatting()
        {
            // given
            var json = JObject.Parse(@"{
                'url': 'http://someserver.com/',
                'hosts': {'host': 'HOST',  'environment': 'ENV'}
             }");

            var props = new CloudProps(json);

            // when
            string host = props.GetCloudHost();

            // then
            Assert.Equal("http://someserver.com", host);
        }

        [Fact]
        public async Task TestReadCloudPropsWithJsonEmptyValuesForUrlAndHostWithUrl()
        {
            // given
            var json = JObject.Parse(@"{
                'hosts': {'host': 'HOST', 'environment': 'ENV', 'url': 'URL_HOST'}
             }");

            var props = new CloudProps(json);

            // when
            string host = props.GetCloudHost();
            string env = props.GetEnv();

            // then
            Assert.Equal("URL_HOST", host);
            Assert.Equal("ENV", env);
        }

        [Fact]
        public async Task TestReadCloudPropsWithJsonEmptyValuesForUrlAndHostWithoutUrl()
        {
            // given
            var json = JObject.Parse(@"{
                'hosts': {'host': 'HOST',  'environment': 'ENV', 'releaseCloudUrl': 'RELEASE_CLOUD_URL'}
             }");
            ServiceFinder.RegisterType<IDeviceService, TestDevice>();

            var props = new CloudProps(json);

            // when
            string host = props.GetCloudHost();
            string env = props.GetEnv();

            // then
            Assert.Equal("RELEASE_CLOUD_URL", host);
            Assert.Equal("ENV", env);
        }


    }
}
