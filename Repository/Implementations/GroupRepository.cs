﻿using AutoMapper;
using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.GroupDTOs;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;
//using System.Text.RegularExpressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public IMapper _mapper;
        public GroupRepository(IMapper mapper, ApplicationDbContext _db) : base(_db)
        {
            _mapper = mapper;
        }
        public async Task<GroupDTO> CreateGroup(GroupUpdate groupDTO)
        {

            //new group automatically sets the created on property to Now
            Group newGroup = _mapper.Map<Group>(groupDTO);
            await CreateEntity(newGroup);
            GroupDTO newGroupDTO = _mapper.Map<GroupDTO>(newGroup);

            return newGroupDTO;
        }
        public async Task<List<GroupDTO>> GetGroups(int userId, PaginationParams? paginationParams = null)
        {
            Expression<Func<Group, bool>> filter = entities => entities.UserId == userId;
            List<Group> groups = await GetAll(filter);
            List<GroupDTO> groupsDTO = _mapper.Map<List<GroupDTO>>(groups);
            return groupsDTO;
        }
        public async Task<GroupDTO> GetGroup(int id, int userId) 
        {
            Expression<Func<Group, bool>> filter = entities => entities.UserId == userId && entities.GroupId == id;
            Group group = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified GorupId not found.");
            GroupDTO groupDTO = _mapper.Map<GroupDTO>(group);
            return groupDTO;
        }
        public async Task<GroupDTO> UpdateGroup(GroupUpdate groupDTOUpdate, int groupId, int userId, bool updateAllFields = false)
        {
            Expression<Func<Group, bool>> expression = entities => entities.GroupId == groupId;
            Group group = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified GorupId not found.");
            await UpdateEntity(group, groupDTOUpdate, updateAllFields);
            GroupDTO groupDTO = _mapper.Map<GroupDTO>(group);
            return groupDTO;
        }
        public async Task DeleteGroup(int groupId, int userId)
        {
            Expression<Func<Group, bool>> expression = entities => entities.GroupId == groupId;
            Group group = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified GorupId not found.");
            await DeleteEntities(group);
        }

    }
}