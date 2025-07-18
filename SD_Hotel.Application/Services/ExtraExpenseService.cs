using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SD_Hotel.Core.Entities.ExtraExpense;

namespace SD_Hotel.Application.Services
{
    public class ExtraExpenseService : IExtraExpenseService
    {
        private readonly IExtraExpenseRepository _extraExpenseRepository;

        public ExtraExpenseService(IExtraExpenseRepository extraExpenseRepository)
        {
            _extraExpenseRepository = extraExpenseRepository;
        }

        public async Task<IEnumerable<ExtraExpenseDto>> GetAllAsync()
        {
            var expenses = await _extraExpenseRepository.GetAllAsync();
            return expenses.Select(MapToDto);
        }

        public async Task<ExtraExpenseDto> GetByIdAsync(int id)
        {
            var expense = await _extraExpenseRepository.GetByIdAsync(id);
            return expense != null ? MapToDto(expense) : null;
        }

        public async Task<ExtraExpenseDto> CreateAsync(CreateExtraExpenseDto createExtraExpenseDto)
        {
            if (Enum.TryParse<ExpenseType>(createExtraExpenseDto.ExpenseType, out var expenseTypeEnum))
            {
                var expense = new ExtraExpense
                {
                    CustomerId = createExtraExpenseDto.CustomerId,
                    ReservationId = createExtraExpenseDto.ReservationId,
                    Description = createExtraExpenseDto.Description,
                    Amount = createExtraExpenseDto.Amount,
                    ExpenseType = expenseTypeEnum,
                    ExpenseDate = createExtraExpenseDto.ExpenseDate,
                    IsPaid = createExtraExpenseDto.IsPaid,
                    PaymentDate = createExtraExpenseDto.PaymentDate,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                await _extraExpenseRepository.AddAsync(expense);
                return MapToDto(expense);
            }
            throw new ArgumentException("Invalid expense type");
        }

        public async Task<ExtraExpenseDto> UpdateAsync(UpdateExtraExpenseDto updateExtraExpenseDto)
        {
            var expense = await _extraExpenseRepository.GetByIdAsync(updateExtraExpenseDto.Id);
            if (expense == null)
                return null;

            if (Enum.TryParse<ExpenseType>(updateExtraExpenseDto.ExpenseType, out var expenseTypeEnum))
            {
                expense.CustomerId = updateExtraExpenseDto.CustomerId;
                expense.ReservationId = updateExtraExpenseDto.ReservationId;
                expense.Description = updateExtraExpenseDto.Description;
                expense.Amount = updateExtraExpenseDto.Amount;
                expense.ExpenseType = expenseTypeEnum;
                expense.ExpenseDate = updateExtraExpenseDto.ExpenseDate;
                expense.IsPaid = updateExtraExpenseDto.IsPaid;
                expense.PaymentDate = updateExtraExpenseDto.PaymentDate;
                expense.UpdatedDate = DateTime.UtcNow;

                await _extraExpenseRepository.UpdateAsync(expense);
                return MapToDto(expense);
            }
            throw new ArgumentException("Invalid expense type");
        }

        public async Task DeleteAsync(int id)
        {
            await _extraExpenseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ExtraExpenseDto>> GetByTypeAsync(string expenseType)
        {
            var expenses = await _extraExpenseRepository.GetByTypeAsync(expenseType);
            return expenses.Select(MapToDto);
        }

        public async Task<IEnumerable<ExtraExpenseDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var expenses = await _extraExpenseRepository.GetByDateRangeAsync(startDate, endDate);
            return expenses.Select(MapToDto);
        }

        public async Task<decimal> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _extraExpenseRepository.GetTotalExpensesAsync(startDate, endDate);
        }

        private static ExtraExpenseDto MapToDto(ExtraExpense expense)
        {
            return new ExtraExpenseDto
            {
                Id = expense.Id,
                CustomerId = expense.CustomerId,
                ReservationId = expense.ReservationId,
                Description = expense.Description,
                Amount = expense.Amount,
                ExpenseType = expense.ExpenseType.ToString(),
                ExpenseDate = expense.ExpenseDate,
                IsPaid = expense.IsPaid,
                PaymentDate = expense.PaymentDate,
                CreatedDate = expense.CreatedDate,
                UpdatedDate = expense.UpdatedDate
            };
        }
    }
} 