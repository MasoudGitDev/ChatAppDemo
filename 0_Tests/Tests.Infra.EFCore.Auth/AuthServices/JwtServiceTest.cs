using FluentAssertions;
using Infra.EfCore.Auth.Constants;
using Infra.EfCore.Auth.Extensions;
using Infra.EfCore.Auth.Services;
using Infra.EFCore.Auth.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.SystemModels;
using Shared.ValueObjects;
using Shared.ValueObjects.Exceptions;
namespace Tests.Infra.EFCore.Auth.AuthServices;
public class JwtServiceTest {
    private const string jwtToken ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWRlbnRpZmllciI6ImJhNmNjMDYzLWRiZWItNDcyOS04YThlLTY2OGVkNmYxNjY3NSIsIlRva2VuSWQiOiJiYzg2YjNhNC0xN2Y3LTQyNjAtYjljYi00MjZmZGI5ZTdkZGYiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDcxIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA3MSIsImlhdCI6IjE3MDY2NDc2MjAiLCJleHAiOiIxNzA2NjUxMjIwIn0.viJxsaVBszbIQwdqfu21tfNme5242Pt1An2pCva7rsw";
    private const string appSettingsPath = @"C:\\Users\\YaZahra\\source\\repos\\ChatAppDemo\\0_Tests\\Tests.Infra.EFCore.Auth\\testAppSettings.json";
    private readonly IConfiguration _configuration;
    private readonly JwtService _jwtServiceClass;
    private readonly AuthTokenSettingsModel _settingModel;
    private static readonly Mock<IAuthTokenService> _mockJwtService  = new();


    public JwtServiceTest() {
        _configuration = new ConfigurationBuilder().AddJsonFile(appSettingsPath).Build();
        _settingModel = _configuration.GetAuthTokenSettingsModel();
        _jwtServiceClass = new JwtService(_configuration);
    }

    [Fact]
    public async Task GenerateTokenAsync_Should_Return_Valid_JWTToken() {
        //Arrange 
        var valid_userId = Guid.NewGuid();
        //Act
        var accountResult = await _jwtServiceClass.GenerateTokenAsync(valid_userId);

        //Assert
        await _jwtServiceClass
            .Invoking(x => x.GenerateTokenAsync(valid_userId))
            .Should().NotThrowAsync<Exception>();
        accountResult.Should().NotBeNull();
        accountResult.AuthToken.Value.Should().NotBeNullOrWhiteSpace();
        accountResult.KeyValueClaims.Should().BeOfType<Dictionary<string , string>>();
        accountResult.KeyValueClaims.Should().HaveCount(6);
    }

    [Fact]
    public async Task GenerateTokenAsync_Should_ThrowEntityIdException_When_UserId_IsInvalid() {
        //Arrange 
        string invalid_Guid = "2";
        await _jwtServiceClass
            .Invoking(x => x.GenerateTokenAsync(invalid_Guid))
            .Should().ThrowAsync<EntityIdException>();
        // can not be continue because of invalid guid.
    }

    [Fact]
    public async Task GetClaimsByTokenAsync_Should_Return_Claims_When_Token_IsValid() {
        //Arrange 
        JwtToken authToken = jwtToken;
        //Act
        var keyValueClaims = await _jwtServiceClass.GetClaimsByTokenAsync(authToken);

        //Assert
        await _jwtServiceClass
          .Invoking(x => x.GetClaimsByTokenAsync(authToken))
          .Should().NotThrowAsync<Exception>();
        keyValueClaims.Should().NotBeNull();
        keyValueClaims.Should().BeOfType<Dictionary<string , string>>();
        keyValueClaims.Should().HaveCount(6);
        keyValueClaims.Should().ContainKeys(JweTypes.UserId , JweTypes.TokenId , "aud" , "iss" , "iat" , "exp");
    }

    [Fact]
    public async Task GetClaimsByTokenAsync_Should_ThrowException_When_Token_IsInvalid() {
        //Arrange
        JwtToken invalidToken = "6as6as5.asa.ad2";

        ////Act
       //var keyValueClaims = await _jwtServiceClass .GetClaimsByTokenAsync(invalidToken);

        //Assert
        await _jwtServiceClass
         .Invoking(x => x.GetClaimsByTokenAsync(invalidToken))
         .Should().ThrowAsync<Exception>();
    }

}
