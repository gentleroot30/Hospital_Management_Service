using HospitalMgmtService.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalMgmtService.Model
{
    public static class DBContextExtension
    {
        public static void seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    Description = "have full access",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1,
                });

            // Doctor, Receptionist, Medical Store Operator, Nurse, 

            // user-admin feature-have full access.
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  UserId = 1,
                  Password = "123",
                  RoleIdFk = 1,
                  Name = "genia",
                  Address = "xyz location",
                  Email = "abc@gmail.com",
                  ContactNo1 = "1234567890",
                  ContactNo2 = "1234567890",
                  ContactNo3 = "1234567890",
                  CreatedAt = DateTime.Now,
                  CreatedBy = 1,
                  UpdatedAt = DateTime.Now,
                  UpdatedBy = 1,
              });


            modelBuilder.Entity<Feature>().HasData(
              new Feature
              {
                  FeatureId = 1,
                  FeatureName = " Admin dashboard ",
                  Description = "Manges all users",
                  CreatedAt = DateTime.Now,
                  UpdatedAt = DateTime.Now,
                  CreatedBy = 1,
                  UpdatedBy = 1,

              });

            modelBuilder.Entity<Feature>().HasData(
               new Feature
               {
                   FeatureId = 2,
                   FeatureName = " User Management ",
                   Description = "Manges all User",
                   CreatedAt = DateTime.Now,
                   UpdatedAt = DateTime.Now,
                   CreatedBy = 1,
                   UpdatedBy = 1,

               });

            modelBuilder.Entity<Feature>().HasData(
               new Feature
               {
                   FeatureId = 3,
                   FeatureName = " Supplier Management ",
                   Description = "Manges all Supplier",
                   CreatedAt = DateTime.Now,
                   UpdatedAt = DateTime.Now,
                   CreatedBy = 1,
                   UpdatedBy = 1,

               });


            modelBuilder.Entity<Feature>().HasData(
               new Feature
               {
                   FeatureId = 4,
                   FeatureName = " Customer Management ",
                   Description = "Manges all Customer",
                   CreatedAt = DateTime.Now,
                   UpdatedAt = DateTime.Now,
                   CreatedBy = 1,
                   UpdatedBy = 1,

               });

            modelBuilder.Entity<Feature>().HasData(
               new Feature
               {
                   FeatureId = 5,
                   FeatureName = " Products Management ",
                   Description = "Manges all Products",
                   CreatedAt = DateTime.Now,
                   UpdatedAt = DateTime.Now,
                   CreatedBy = 1,
                   UpdatedBy = 1,

               });
            modelBuilder.Entity<Feature>().HasData(
              new Feature
              {
                  FeatureId = 6,
                  FeatureName = " Purchase Management ",
                  Description = "Manges all Purchase",
                  CreatedAt = DateTime.Now,
                  UpdatedAt = DateTime.Now,
                  CreatedBy = 1,
                  UpdatedBy = 1,

              });

            modelBuilder.Entity<Feature>().HasData(
              new Feature
              {
                  FeatureId = 7,
                  FeatureName = " Sales Management ",
                  Description = "Manges all Sales",
                  CreatedAt = DateTime.Now,
                  UpdatedAt = DateTime.Now,
                  CreatedBy = 1,
                  UpdatedBy = 1,

              });

            modelBuilder.Entity<Feature>().HasData(
              new Feature
              {
                  FeatureId = 8,
                  FeatureName = " Expenses Management ",
                  Description = "Manges all Expenses",
                  CreatedAt = DateTime.Now,
                  UpdatedAt = DateTime.Now,
                  CreatedBy = 1,
                  UpdatedBy = 1,

              });

            modelBuilder.Entity<Feature>().HasData(
             new Feature
             {
                 FeatureId = 9,
                 FeatureName = " Reports Management ",
                 Description = "Manges all Reports",
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now,
                 CreatedBy = 1,
                 UpdatedBy = 1,

             });


            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk =1,
                    RoleIdFk = 1,
                    ViewPerm= true,
                    AddPerm= false,
                    EditPerm=false,
                    DeletePerm=false,
                    CreatedAt= DateTime.Now,
                    CreatedBy= 1,
                    UpdatedBy= 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 2,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 3,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 4,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 5,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 6,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 7,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 8,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = true,
                    EditPerm = true,
                    DeletePerm = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });

            modelBuilder.Entity<RoleFeature>().HasData(
                new RoleFeature
                {
                    FeatureIdFk = 9,
                    RoleIdFk = 1,
                    ViewPerm = true,
                    AddPerm = false,
                    EditPerm = false,
                    DeletePerm = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                });


        }


    }
}
