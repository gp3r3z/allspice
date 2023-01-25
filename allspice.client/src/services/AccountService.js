import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  // TODO working on getting my favorites
  async getMyFavorite() {
    logger.log('FE getting recipe by account')

    const res = await api.get(`account/favorites`)

    const responseData = res.data
    logger.log('FE response', responseData)


    // let favRecipes = AppState.recipes.filter(r => responseData.filter(rd => console.log(rd.recipeId == r.id)))
    let favRecipes = responseData.filter(r => r.recipeId == AppState.recipes.id)

    // AppState.recipes = res.data
    logger.log("Here are the recipes ", favRecipes)

  }
}

export const accountService = new AccountService()
