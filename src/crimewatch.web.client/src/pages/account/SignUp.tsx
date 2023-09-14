import { useState } from "react";
import { Gender } from "../../enums/Gender"
import { WitnessDto } from "../../models/Witness";

const SignUp = () => {
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [gender, setGender] = useState<Gender>(Gender.Male);
  const [dateOfBirth, setDateOfBirth] = useState<string>("");
  const [phoneNumber, setPhoneNumber] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const baseURL = import.meta.env.BASE_URL;
    console.log(baseURL);
    if (password !== confirmPassword) {
      alert("Password and Confirm Password do not match");
      return;
    }

    const witness : WitnessDto = {
      firstName: firstName,
      lastName: lastName,
      gender:gender,
      birthDate: new Date(dateOfBirth),
      phoneNumber: phoneNumber,
      email: email,
      password: password,
    }
    console.log(witness);
  }

  const content : JSX.Element = <>
  <div className="h-[80px]"></div>
  <div className="h-[80px]">
    <h1 className="text-4xl text-center">Sign Up</h1>
    <div className="flex flex-col sm:flex-row items-center justify-center gap-5">
      <form id="formSection" className="border border-black/5 p-5"
        onSubmit={handleSubmit} >
        <div id="form-firstName" className="form-group">
          <label htmlFor="firstName">First Name: </label>
          <input required type="text" name="firstName" id="firstName" className="form-input" 
            onChange={e=> setFirstName(e.target.value)} value={firstName} />
        </div>
        <div id="form-lastName" className="form-group">
          <label htmlFor="lastName">Last Name: </label>
          <input required type="text" name="lastName" id="lastName" className="form-input"
            onChange={e=>setLastName(e.target.value)} value={lastName}/>
        </div>
        <div id="form-gender" className="form-group flex">
          <label htmlFor="gender">Gender: </label>
          <div id="form-gender-content" className="flex gap-x-5">
            <div>
              <label htmlFor="gender-male">Male </label>
              <input required aria-label="Male" type="radio" name="gender" value={Gender.Male}
                onChange={e=>setGender(Number(e.target.value))}
                checked={gender === Gender.Male} />
            </div>
            <div>
              <label htmlFor="gender-male">Female </label>
              <input required aria-label="Female" type="radio" name="gender" value={Gender.Female}
                onChange={e=>setGender(Number(e.target.value))}
                checked={gender === Gender.Female} />
            </div>
          </div>
        </div>
        <div id="form-dateOfBirth" className="form-group">
          <label htmlFor="dateOfBirth">Date of Birth: </label>
          <input required type="date" name="dateOfBirth" id="dateOfBirth"
            onChange={e=>setDateOfBirth(e.target.value)}
            value={dateOfBirth} />
        </div>
        <div id="form-phoneNumber" className="form-group">
          <label htmlFor="phoneNumber">Phone Number: </label>
          <input required type="text" name="phoneNumber" id="phoneNumber" className="form-input"
            onChange={e=>setPhoneNumber(e.target.value)} value={phoneNumber}/>
        </div>
        <div id="form-email" className="form-group">
          <label htmlFor="email">Email: </label>
          <input required type="email" name="email" id="email" className="form-input" 
            onChange={e=>setEmail(e.target.value)}
            value={email}/>
        </div>
        <div id="form-password" className="form-group">
          <label htmlFor="password">Password: </label>
          <input required type="password" name="password" id="password" className="form-input" 
          onChange={e=>setPassword(e.target.value)}
          value={password} />
        </div>
        <div id="form-confirmPassword" className="form-group">
          <label htmlFor="confirmPassword">Password: </label>
          <input required type="password" name="confirmPassword" id="confirmPassword" className="form-input" 
          onChange={e=>setConfirmPassword(e.target.value)}
          value={confirmPassword} />
        </div>
        <div id="form-submit" className="form-group flex gap-x-5">
          <label></label>
          <button type="submit" className="px-3 py-3" >Signup</button>
          <button type="reset" className="px-3 py-3 text-red-600" >Reset</button>
        </div>
      </form>
    </div>
  </div>
  </>
  return content;
};
export default SignUp