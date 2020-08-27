// <copyright file="DepositInput.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.UseCases.Deposit
{
    using System;
    using Domain.ValueObjects;
    using Services;

    /// <summary>
    ///     Deposit Input Message.
    /// </summary>
    internal sealed class DepositInput
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DepositInput" /> class.
        /// </summary>
        /// <param name="accountId">AccountId.</param>
        /// <param name="amount">Positive amount to deposit.</param>
        /// <param name="currency">Currency from amount.</param>
        internal DepositInput(Guid accountId, decimal amount, string currency)
        {
            this.ModelState = new Notification();

            if (accountId != Guid.Empty)
            {
                this.AccountId = new AccountId(accountId);
            }
            else
            {
                this.ModelState.Add(nameof(accountId), "AccountId is required.");
            }

            if (currency == Currency.Dollar.Code ||
                currency == Currency.Euro.Code ||
                currency == Currency.BritishPound.Code ||
                currency == Currency.Canadian.Code ||
                currency == Currency.Real.Code ||
                currency == Currency.Krona.Code)
            {
                if (amount > 0)
                {
                    this.Amount = new PositiveMoney(amount, new Currency(currency));
                }
                else
                {
                    this.ModelState.Add(nameof(amount), "Amount should be positive.");
                }
            }
            else
            {
                this.ModelState.Add(nameof(currency), "Currency is required.");
            }
        }

        internal AccountId AccountId { get; }
        internal PositiveMoney Amount { get; }
        internal Notification ModelState { get; }
    }
}
