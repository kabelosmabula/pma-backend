using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Household.InvoiceStatements.Command
{
    public class GenerateHouseholdStatementCommand : IRequest<Result<string>>
    {
        public string HouseholdId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
