using FBData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FBData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;

namespace AFCAPI.Controllers
{

    public static class PlayerEndpoints
    {
        public static void MapPlayerEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Player").WithTags(nameof(Player));

            group.MapGet("/", async (FBContext db) =>
            {
                return await db.Players.ToListAsync();
            })
            .WithName("GetAllPlayers")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<Player>, NotFound>> (int playerid, FBContext db) =>
            {
                return await db.Players.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.PlayerId == playerid)
                    is Player model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetPlayerById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int playerid, Player player, FBContext db) =>
            {
                var affected = await db.Players
                    .Where(model => model.PlayerId == playerid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(m => m.PlayerId, player.PlayerId)
                        .SetProperty(m => m.PlayerName, player.PlayerName)
                        .SetProperty(m => m.Position, player.Position)
                        .SetProperty(m => m.GoalScored, player.GoalScored)
                        );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdatePlayer")
            .WithOpenApi();

            group.MapPost("/", async (Player player, FBContext db) =>
            {
                db.Players.Add(player);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Player/{player.PlayerId}", player);
            })
            .WithName("CreatePlayer")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int playerid, FBContext db) =>
            {
                var affected = await db.Players
                    .Where(model => model.PlayerId == playerid)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeletePlayer")
            .WithOpenApi();
        }
    }
}
