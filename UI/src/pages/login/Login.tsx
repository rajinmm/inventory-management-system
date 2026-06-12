import { useState } from "react"
import { Button } from "@progress/kendo-react-buttons"
import { TextBox } from "@progress/kendo-react-inputs"
import { useNavigate } from "react-router-dom"
import { useLoginUserMutation } from "../../apiutils/auth/authService"
import { LoginRequest } from "../../apiutils/auth/types";

export const Login: React.FC = () => {
  const navigate = useNavigate();
  const [loginUser] = useLoginUserMutation();
  const [userLogin, setuserLogin] = useState("");
  const [password, setPassword] = useState("");




    const handleLogin =  async () => {

    if (!userLogin || !password) {
      alert("Username and Password required");
      return;
    }
    try {
      const userData =  await  loginUser({
        userLogin: userLogin,
        password: password
       } as LoginRequest).unwrap();
       if (userData) {
        navigate("/dashboard");
       }
       else
       {
        alert("Login failed. Please check your credentials.");
       }

       
      
    } catch (error) {
      console.error("Login failed:", error)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white shadow-xl rounded-2xl p-10 w-[400px]">
        <h2 className="text-2xl font-bold text-center mb-6">
          ** Ram Stores **         
        </h2>
         <div className="mb-5 relative">
               <TextBox
               value={ userLogin }
                onChange={(e) => setuserLogin(e.value?.toString() ?? "")}
            
            
            placeholder="USERNAME"
           
          />
         </div>
   <div className="mb-5 relative">
      <TextBox
            value={password}
            onChange={(e) => setPassword(e.value?.toString() ?? "")}
            type="password"
            placeholder="PASSWORD"
          />
   </div>
    <Button onClick={handleLogin}

          className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 transition duration-300">
        Login
        </Button>

        {/* Forgot */}

        <div className="text-center mt-4 text-sm text-black/80 cursor-pointer hover:underline">
          Forgot password?
        </div>



       

      </div>
    </div>
  )
}