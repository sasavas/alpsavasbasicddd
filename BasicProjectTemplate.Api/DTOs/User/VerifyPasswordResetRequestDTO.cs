namespace BasicProjectTemplate.Api.DTOs.User;

public record VerifyPasswordResetRequestDTO(Guid code, string newPassword);