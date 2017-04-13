using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warrior.Handlers.Enums
{
    public enum TransactionResult
    {
        OK,
        CREATED,
        UPDATED,
        DELETED,
        ERROR,
        EXISTS,
        NOT_PERMITTED
    }
}