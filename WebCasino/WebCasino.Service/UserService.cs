﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
    public class UserService : IUserService
    {
        private readonly CasinoContext context;
        public UserService(CasinoContext context)
        {
            this.context = context;
        }

        public Task<User> EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await this.context.Users.Where(us => !us.Locked).ToListAsync();
        }

        public async Task<User> LockUser(string id)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(us => us.Id == id && !us.IsDeleted);
            ServiceValidator.ObjectIsNotEqualNull(user);
            user.Locked = true;
            await this.context.SaveChangesAsync();
            return user;
        }

        public async Task<User> PromoteUser(string id)
        {
            //var user = await this.context.Users.FirstOrDefaultAsync(us => us.Id == id && !us.IsDeleted);
            //ServiceValidator.ObjectIsNotEqualNull(user);
            ////var userRole = this.context.Role
            throw new NotImplementedException();
        }

        public async Task<User> RetrieveUser(string id)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(us => us.Id == id && !us.Locked);
            if(user == null)
            {
                throw new EntityNotFoundException("User not found");
            }
            return user;
        }
    }
}