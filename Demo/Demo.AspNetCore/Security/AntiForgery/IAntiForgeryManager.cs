﻿namespace Demo.AspNetCore.Security.AntiForgery
{
    public interface IAntiForgeryManager
    {
        IAntiForgeryConfiguration Configuration { get; }

        string GenerateToken();
    }
}