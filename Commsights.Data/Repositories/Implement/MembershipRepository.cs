﻿using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commsights.Data.Repositories
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        private readonly CommsightsContext _context;

        public MembershipRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public bool IsExistEmail(string email)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Email.Equals(email));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistPhone(string phone)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Phone.Equals(phone));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsLoginByAccount(string account, string password)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Account.Equals(account));
            if (membership == null)
            {
                membership = _context.Membership.FirstOrDefault(user => user.Email.Equals(account));
            }
            if (membership == null)
            {
                membership = _context.Membership.FirstOrDefault(user => user.Phone.Equals(account));
            }
            if (membership != null)
            {
                if (SecurityHelper.Decrypt(membership.Guicode, membership.Password).Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsLoginByID(int ID, string password)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.ID == ID);
            if (membership != null)
            {
                if (SecurityHelper.Decrypt(membership.Guicode, membership.Password).Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
