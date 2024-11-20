namespace Prog_P2_claims.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }

        // could add a foreignkey 
        //public int ClaimID get set 

        //nvigate proptery 
        //public Claims Claims get ste 

    }
}
