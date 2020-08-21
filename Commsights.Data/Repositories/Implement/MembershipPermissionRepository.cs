using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Commsights.Data.Repositories
{
    public class MembershipPermissionRepository : Repository<MembershipPermission>, IMembershipPermissionRepository
    {
        private readonly CommsightsContext _context;

        public MembershipPermissionRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<MembershipPermission> GetByMembershipIDToList(int membershipID)
        {
            return _context.MembershipPermission.Where(item => item.MembershipId == membershipID).OrderBy(item => item.Id).ToList();
        }
        public void InitializationMenuPermission(int membershipID, int requestUserID)
        {
            List<MembershipPermission> list = _context.MembershipPermission.Where(item => item.MembershipId == membershipID && item.MenuId > 0).ToList();
            _context.MembershipPermission.RemoveRange(list);
            List<Config> listMenu = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(AppGlobal.Menu)).OrderBy(item => item.SortOrder).ToList();
            foreach (Config menu in listMenu)
            {
                MembershipPermission membershipPermission = new MembershipPermission();
                membershipPermission.MembershipId = membershipID;
                membershipPermission.MenuId = menu.Id;
                membershipPermission.IsView = false;
                membershipPermission.Initialization(InitType.Insert, requestUserID);
                _context.MembershipPermission.Add(membershipPermission);
            }
            _context.SaveChanges();
        }
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID)
        {
            List<MembershipPermission> list = _context.MembershipPermission.Where(item => item.MembershipId == membershipID && item.MenuId > 0).ToList();
            foreach (MembershipPermission membershipPermission in list)
            {
                membershipPermission.IsView = isAll;
                membershipPermission.Initialization(InitType.Update, requestUserID);
                _context.MembershipPermission.Update(membershipPermission);
            }
            _context.SaveChanges();
        }
        public bool IsExistByMembershipIDAndBrandID(int membershipID, int brandID)
        {
            var membershipPermission = _context.MembershipPermission.FirstOrDefault(item => item.MembershipId == membershipID && item.BrandId == brandID);
            if (membershipPermission != null)
            {
                return true;
            }
            return false;
        }
    }
}
