namespace WordFrequency.Domain.ValueObjects;

public record TextInput
{
    private const int MaxLength = 2048;

    public string Value { get; }

    private TextInput(string value) => Value = value;

    public static Result<TextInput> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Result<TextInput>.Failure("Text cannot be empty");

        if (value.Length > MaxLength)
            return new Result<TextInput>.Failure($"Text cannot exceed {MaxLength} characters");

        return new Result<TextInput>.Success(new TextInput(value.Trim()));
    }
}

public abstract record Result<T>
{
    public sealed record Success(T Value) : Result<T>;
    public sealed record Failure(string Error) : Result<T>;

    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<string, TResult> onFailure) =>
        this switch
        {
            Success s => onSuccess(s.Value),
            Failure f => onFailure(f.Error),
            _ => throw new InvalidOperationException()
        };
}

file static class ResultFactory
{
    public static Result<T> Success<T>(T value) => new Result<T>.Success(value);
    public static Result<T> Failure<T>(string error) => new Result<T>.Failure(error);
}
