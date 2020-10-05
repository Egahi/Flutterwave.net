﻿using Flutterwave.Net;
using Flutterwave.Net.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace flutterwave_dotnet_test.Apis
{
    public class BanksTests
    {
        private BankService _banks;

        public BanksTests()
        {
            // Get wave secret key from environmental variables
            var flutterwaveSecretKey = Environment.GetEnvironmentVariable("FlutterwaveSecretKey");

            _banks = new BankService(new FlutterwaveApi(flutterwaveSecretKey));
        }

        [Fact]
        public void GetBanks_InvalidSecretKey_ReturnsError()
        {
            // Arrange
            var flutterwaveSecretKey = "";
            _banks = new BankService(new FlutterwaveApi(flutterwaveSecretKey));
            
            // Act
            var result = _banks.GetBanks(Country.Nigeria);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetBanksResponse>(result);
            Assert.Equal(expected: AppConstants.ERROR_STATUS, actual: result.Status);
            Assert.Equal(expected: AppConstants.UNAUTHORIZED_MESSAGE, actual: result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetBanks_ValidSecretKey_ReturnsBanks()
        {
            // Arrange
            var flutterwaveSecretKey = "FLWSECK_TEST-SANDBOXDEMOKEY-X";
            _banks = new BankService(new FlutterwaveApi(flutterwaveSecretKey));

            // Act
            var result = _banks.GetBanks(Country.Nigeria);

            // Assert
            Assert.NotNull(result); 
            Assert.IsType<GetBanksResponse>(result);
            Assert.Equal(expected: AppConstants.SUCCESS_STATUS, actual: result.Status);
            Assert.Equal(expected: AppConstants.GET_BANKS_SUCCESS_MESSAGE, actual: result.Message);
            Assert.IsType<List<Bank>>(result.Data);
        }
    }
}