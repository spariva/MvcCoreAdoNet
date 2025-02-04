using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryDoctor
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public RepositoryDoctor()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string sql = "select * from doctor";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Doctor> doctors = new List<Doctor>();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctors.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctors;
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(string especialidad)
        {
            string sql = "select * from doctor where especialidad=@especialidad";
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Doctor> doctors = new List<Doctor>();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor {
                    IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString()),
                    Apellido = this.reader["APELLIDO"].ToString(),
                    Especialidad = this.reader["ESPECIALIDAD"].ToString(),
                    Salario = int.Parse(this.reader["SALARIO"].ToString()),
                    IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString())
                };
                doctors.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return doctors;
        }

        public async Task<List<string>> GetEspecialidadesAsync()
        {
            string sql = "select distinct especialidad from doctor";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<string> especialidades = new List<string>();
            while (await this.reader.ReadAsync())
            {
                especialidades.Add(this.reader["ESPECIALIDAD"].ToString());
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return especialidades;
        }
    }
}
