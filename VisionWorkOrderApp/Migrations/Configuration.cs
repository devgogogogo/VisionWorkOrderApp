namespace VisionWorkOrderApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VisionWorkOrderApp.Models.VisionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VisionWorkOrderApp.Models.VisionDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

/*
 * Enable-Migrations
 * Enable-Migrations
→ JPA 의 ddl-auto 설정 같은 것
→ EF 가 DB 변경사항을 추적할 수 있게 준비!
 */


//Add-Migration InitialCreate
/*
 * Add-Migration  →  JPA 의 ddl-auto=create 같은 것
InitialCreate  →  이 Migration 의 이름 (자유롭게 지어도 됨)

→ WorkOrder, Equipment 클래스를 보고
→ 테이블 생성 스크립트를 자동으로 만들어줘요!
 */

/*
 * Update-Database
 * Update-Database
→ InitialCreate 스크립트를 실행해서
→ VisionMES DB 에 실제 테이블 생성!
 */