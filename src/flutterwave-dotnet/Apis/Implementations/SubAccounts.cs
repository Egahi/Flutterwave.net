﻿using Flutterwave.Net.Utilities;

namespace Flutterwave.Net
{
    public class SubAccounts : ISubAccounts
    {
        private FlutterwaveApi _flutterwaveApi { get; }

        public SubAccounts(FlutterwaveApi flutterwaveApi)
        {
            _flutterwaveApi = flutterwaveApi;
        }

        /// <summary>
        /// Create a subaccount
        /// </summary>
        /// <param name="bankCode">
        /// This is the sub-accounts bank ISO code. 
        /// It is returned in the Get banks call as data.code
        /// </param>
        /// <param name="accountNumber">This is the customer's account number.</param>
        /// <param name="businessName">This is the sub-account business name.</param>
        /// <param name="businessEmail">This is the sub-account business email</param>
        /// <param name="country">Merchant's country</param>
        /// <param name="splitType">This can be set as percentage or flat</param>
        /// <param name="splitValue">
        /// This can be a percentage value or flat value depending on what was set on splitType. 
        /// Note that the % value is in decimal. So 50% is 0.5 and so on.
        /// </param>
        /// <param name="businessContact">This is the contact person for the sub-account</param>
        /// <param name="businessContactMobile">Business contact phone number</param>
        /// <param name="businessMobile">Primary business phone number</param>
        /// <returns>The newly created sub account details</returns>
        public CreateSubAccountResponse CreateSubAccount(string bankCode,
                                                         string accountNumber,
                                                         string businessName,
                                                         string businessEmail,
                                                         Country country,
                                                         SplitType splitType,
                                                         double splitValue,
                                                         string businessContact = "",
                                                         string businessContactMobile = "",
                                                         string businessMobile = "")
        {
            var data = new CreateSubAccountRequest(bankCode,
                                                   accountNumber,
                                                   businessName,
                                                   businessEmail,
                                                   country.GetValue(),
                                                   splitType.GetValue(),
                                                   splitValue,
                                                   businessContact,
                                                   businessContactMobile,
                                                   businessMobile);

            return _flutterwaveApi.Post<CreateSubAccountResponse>(Endpoints.SUB_ACCOUNTS, data);
        }

        /// <summary>
        /// Delete a sub account
        /// </summary>
        /// <param name="subAccountId">
        /// The unique id of the sub account you want to delete, it is returned in the Get SubAccount call as data.id
        /// </param>
        /// <returns>Success message</returns>
        public DeleteSubAccountResponse DeleteSubAccount(int subAccountId)
        {
            return _flutterwaveApi.Delete<DeleteSubAccountResponse>($"{Endpoints.SUB_ACCOUNTS}/{subAccountId}");
        }

        /// <summary>
        /// Get all sub accounts
        /// </summary>
        /// <returns>A list of sub accounts</returns>
        public GetSubAccountsResponse GetSubAccounts()
        {
            return _flutterwaveApi.Get<GetSubAccountsResponse>(Endpoints.SUB_ACCOUNTS);
        }

        /// <summary>
        /// Get SubAccounts
        /// </summary>
        /// <param name="id">
        /// Unique SubAccounts ID, it is returned in the Get SubAccounts call as data.id
        /// </param>
        /// <returns>A SubAccounts</returns>
        public GetSubAccountsByIdResponse GetSubAccountsById(int id)
        {
            return _flutterwaveApi.Get<GetSubAccountsByIdResponse>($"{Endpoints.SUB_ACCOUNTS}/{id}");
        }
    }
}
