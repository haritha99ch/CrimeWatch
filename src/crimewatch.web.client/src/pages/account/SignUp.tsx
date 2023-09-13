import { Gender } from "../../enums/Gender"

const SignUp = () => {
  return <>
    <div className="h-[80px]"></div>
    <div className="h-[80px]">
      <h1 className="text-4xl text-center">Sign Up</h1>
      <div className="flex flex-col sm:flex-row items-center justify-center gap-5">
        <div id="formSection" className="border border-black/5 p-5">
          <div id="form-firstName" className="form-group">
            <label htmlFor="firstName">First Name: </label>
            <input type="text" name="firstName" id="firstName" className="form-input" />
          </div>
          <div id="form-lastName" className="form-group">
            <label htmlFor="lastName">Last Name: </label>
            <input type="text" name="lastName" id="lastName" className="form-input" />
          </div>
          <div id="form-gender" className="form-group flex">
            <label htmlFor="gender">Gender: </label>
            <div id="form-gender-content" className="flex gap-x-5">
              <div>
                <label htmlFor="gender-male">Male </label>
                <input type="radio" name="gender" value={Gender.Male} />
              </div>
              <div>
                <label htmlFor="gender-male">Female </label>
                <input type="radio" name="gender" value={Gender.Female} />
              </div>
            </div>
          </div>
          <div id="form-dateOfBirth" className="form-group">
            <label htmlFor="dateOfBirth">Date of Birth: </label>
            <input type="date" name="dateOfBirth" id="dateOfBirth" />
          </div>
          <div id="form-email" className="form-group">
            <label htmlFor="email">Email: </label>
            <input type="email" name="email" id="email" className="form-input" />
          </div>
          <div id="form-password" className="form-group">
            <label htmlFor="password">Password: </label>
            <input type="password" name="password" id="password" className="form-input" />
          </div>
        </div>
      </div>
    </div>
</>
}
export default SignUp