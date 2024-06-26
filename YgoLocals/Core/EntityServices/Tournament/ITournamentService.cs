﻿namespace YgoLocals.Core.EntityServices.Tournament
{
    using YgoLocals.Models.Tournament;
    using YgoLocals.Data.Entities;

    public interface ITournamentService : IBaseService<int, BaseTournamentViewModel>
    {
        Task<IList<BaseTournamentViewModel>> GetAllByUserAsync(string userId);

        Task<int> CreateAsync(string organizerId, int maxPeopleCount, int tournamentTypeId);

        Task<bool> JoinPlayerAsync(int tournamentId, string playerId, IList<string> deckIds);

        Task<IList<BaseTournamentViewModel>> GetAllAvaiableToJoinAsync();

        Task<Tournament> StartAsync(int tournamentId, string userId);

        Task<Tournament> GetEntityById(int tournamentId);

        Task SetIdlePlayerAsync(Tournament tournament, string playerId);
    }
}
