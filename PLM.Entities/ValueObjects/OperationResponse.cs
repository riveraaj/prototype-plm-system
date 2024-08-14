namespace PLM.Entities.ValueObjects;
public record struct OperationResponse(List<object> Content, short Code = 1,
                                       string Message = "No se ha recibido ningún mensaje");