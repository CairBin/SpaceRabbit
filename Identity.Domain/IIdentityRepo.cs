using System;
using Microsoft.AspNetCore.Identity;
using IdentityService.Domain.Entity;


namespace IdentityService.Domain
{
    public interface IIdentityRepo
    {
        /// <summary>
        /// 根据GUID获取用户
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <returns></returns>
        Task<User?> FindByIdAsync(Guid userId);
        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        Task<User?> FindByUsernameAsync(string username);

        /// <summary>
        /// 根据邮箱查找用户
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        Task<User?> FindByEmailAsync(string email);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(User username,string password);

        /// <summary>
        /// 记录一次登陆失败
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        Task<IdentityResult> AccessFailedAsync(User user);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string password);

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        Task<IList<string>> GetRolesAsync(User user);

        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        Task<IdentityResult> AddToRoleAsync(User user, string role);

        /// <summary>
        /// 检查登录用户名、密码是否正确
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="lockoutOnFailure">如果登陆失败，则记录一次失败</param>
        /// <returns></returns>
        public Task<SignInResult> CheckSignInAsync(User user, string password, bool lockoutOnFailure);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        public Task<IdentityResult> RemoveUserAsync(Guid id);

        /// <summary>
        /// 添加管理员用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public Task<(IdentityResult, User?, string password)> AddAdminAsync(string username,string email);

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <param name="email">新邮箱地址</param>
        /// <returns></returns>
        public Task UpdateEmail(Guid id, string email);

    }
}
