﻿using Common;
using DataStorage;
using DataEntities;
using FamilyDto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cards.Services
{
    public class CardService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public CardService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task<IQueryable<CardDto>> GetAllCardsForUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            return _dbContext.Cards
                 .Where(x => x.UserId == userId)
                 .Where(card => card.UserId == userId)
                 .Select(card => _serviceMapper.Map<Card, CardDto>(card));
        }

        public async Task<CardDto> GetCardAsync(int userId, int cardId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            var card = user.Cards.FirstOrDefault(x => x.Id == cardId);
            return _serviceMapper.Map<Card, CardDto>(card);
        }

        public async Task<CardDto> CreateCardsForUserAsync(int userId, CardDto cardDto)
        {
            var user = await _dbContext.Users
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException($"This user {userId} does not exist");
            }

            var cardEntityAdd = _serviceMapper.Map<CardDto, Card>(cardDto);
            user.Cards.Add(cardEntityAdd);
            await _dbContext.SaveChangesAsync();
            return _serviceMapper.Map<Card, CardDto>(cardEntityAdd);
        }

        public async Task DeleteCardAsync(int userId, int cardId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var card = _dbContext.Cards.FirstOrDefault(x => x.Id == cardId);
            if (user == null || card == null || card.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or card {card.Id} is not found \n Change data");
            }

            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CardDto> PatchCardAsync(int userId, CardDto cardDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var card = _dbContext.Cards.FirstOrDefault(x => x.Id == cardDto.Id);
            
            if (user == null || card == null || card.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or card {card.Id} is not found \n Change data");
            }

            var cardMerge = _serviceMapper.Merge(cardDto, card);
            await _dbContext.SaveChangesAsync();
            var newCardMergeMap = _serviceMapper.Map<Card, CardDto>(cardMerge);
            return newCardMergeMap;
        }
    }
}
