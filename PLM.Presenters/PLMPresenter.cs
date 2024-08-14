namespace PLM.Presenters;
/// <summary>
/// Represents a presenter that handles operation responses.
/// </summary>
internal class PLMPresenter : IOutputPort
{
    public OperationResponse OperationResponse { get; private set; }

    /// <summary>
    /// Handles the given operation response by setting the internal property.
    /// </summary>
    /// <param name="oOperationResponse">The operation response to be handled.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task Handle(OperationResponse oOperationResponse)
    {
        OperationResponse = oOperationResponse;
        return Task.CompletedTask;
    }
}