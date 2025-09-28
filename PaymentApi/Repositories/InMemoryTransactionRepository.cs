using PaymentApi.Models;
using System.Collections.Concurrent;

namespace PaymentApi.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly ConcurrentDictionary<string, TransactionRecord> _store = new();

        public Task SaveAsync(TransactionRecord tx)
        {
            _store[tx.Id.ToString()] = tx;
            return Task.CompletedTask;
        }

        public Task<TransactionRecord?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var tx);
            return Task.FromResult(tx);
        }

        public Task<TransactionRecord?> GetByPaymentIntentIdAsync(string paymentIntentId)
        {
            foreach (var kv in _store.Values)
                if (kv.PaymentIntentId == paymentIntentId) return Task.FromResult<TransactionRecord?>(kv);
            return Task.FromResult<TransactionRecord?>(null);
        }
    }
}