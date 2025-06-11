﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace MyPortfolioWebApp.Models
{
    // DbContext : 직접 쿼리를 연결하고, 명령객체(MySqlCommand)로 처리하는 게 아니고
    // 일련의 CRUD 작업을 모두 래핑해서 사용할 수 있도록 만들어놓은 클래스
    // CRUD 쿼리를 직접 작성할 필요가 없음
    // ASP.NET Core Identity를 사용하려면 DbContext에서 IdentityDbContext로 변경
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        // 아래에 DB와 연동할 모델폴더내 클래스를 선언필수
        // 이거 넣고 나서 NuGet 패키지 관리자 콘솔에서 Add-Migration 수행
        // 새로추가된 DB테이블 모델이 있으면 반드시 아래에 추가!!
        public DbSet<News> News { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Skill> Skill { get; set; }
    }
}
