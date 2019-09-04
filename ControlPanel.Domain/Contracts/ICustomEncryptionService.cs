namespace ControlPanel.Domain.Contracts
{
    public interface ICustomEncryptionService
    {
        string DecryptStringFromBytes(byte[] cipherText);
        byte[] EncryptStringToBytes(string plainText);
    }
}
