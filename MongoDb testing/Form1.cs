using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb_testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Users");
            var collection = database.GetCollection<BsonDocument>("Users");
            var document = new BsonDocument { { "username", textBox1.Text }, { "password", textBox2.Text } };
            await collection.InsertOneAsync(document);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var filter1 = Builders<BsonDocument>.Filter.Eq("username", textBox1.Text) & Builders<BsonDocument>.Filter.Eq("password", textBox2.Text);
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("Users");
                var collection = database.GetCollection<BsonDocument>("Users");
                var user = collection.Find(filter1).FirstOrDefault();
                if (user == null)
                {
                    System.Windows.Forms.MessageBox.Show("User does not exist or wrong creds");
                }
                if (user != null)
                {
                    System.Windows.Forms.MessageBox.Show("User Exists");
                }
            }
            catch
            {
                listBox1.Items.Add("User not found");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var deleteFilter = Builders<BsonDocument>.Filter.Eq("username", textBox1.Text);
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("Users");
                var collection = database.GetCollection<BsonDocument>("Users");
                var user = collection.Find(deleteFilter).FirstOrDefault();
                collection.DeleteOne(deleteFilter);
            }
            catch
            {
                listBox1.Items.Add("User not found");
            }
        }
    }
}