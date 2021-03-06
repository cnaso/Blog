﻿using Blog.Areas.Admin.ViewModels;
using Blog.Core.Domain;
using Blog.Infrastructure;
using Blog.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Selected("users")]
    public class UsersController : Controller
    {
        private BlogContext _context;

        public UsersController(BlogContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(new UserList
            {
                Users = _context.Users.ToList()
            });
        }

        public ActionResult New()
        {
            return View(new NewUser()
            {
                Roles = _context.Roles.ToList().Select(role => new RoleCheckbox
                {
                    Id = role.Id,
                    IsChecked = false,
                    Name = role.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(NewUser form)
        {
            if (_context.Users.Any(u => u.Username == form.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken, choose a different username.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var user = new User();
            user.Email = form.Email;
            user.Username = form.Username;
            user.SetPassword(form.Password);

            SyncUserRoles(form.Roles, user.Roles);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new EditUser
            {
                Username = user.Username,
                Email = user.Email,
                Roles = _context.Roles.ToList().Select(role => new RoleCheckbox
                {
                    Id = role.Id,
                    IsChecked = user.Roles.Contains(role),
                    Name = role.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditUser form)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (_context.Users.Any(u => u.Username == form.Username && u.Id != id))
            {
                ModelState.AddModelError("Username", "Username is already taken, choose a different username.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Username = form.Username;
            user.Email = form.Email;

            SyncUserRoles(form.Roles, user.Roles);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new ResetUserPassword
            {
                Username = user.Username
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, ResetUserPassword form)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            form.Username = user.Username;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.SetPassword(form.Password);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void SyncUserRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {
            foreach (var role in _context.Roles.ToList())
            {
                var checkbox = checkboxes.Single(c => c.Id == role.Id);

                if (checkbox.IsChecked)
                {
                    roles.Add(role);
                }
                else
                {
                    roles.Remove(role);
                }
            }
        }
    }
}