﻿using Application.Models.Dtos.TeacherDtos;

namespace Application.Interfaces.Services;

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetAllAsync();
    Task<TeacherDto> GetByIdAsync(int id);
    Task<TeacherDto> GetByUserIdAsync(string userId);
    Task<TeacherDto> AddAsync(AddTeacherDto entity);
    Task<bool> DeleteAsync(int id, string userId);
    Task<TeacherDto> UpdateAsync(int id, UpdateTeacherDto entity);
}
