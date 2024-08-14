namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IDesignCommentRepository
{
    public Task<OperationResponse> GetByDesignIdAsync(int id);
    public Task<OperationResponse> CreateAsync(DesignComment oDesignComment);
}